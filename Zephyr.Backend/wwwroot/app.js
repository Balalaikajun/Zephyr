const providerSelect = document.getElementById('provider');
const locationInput = document.getElementById('location');
const suggestionsList = document.getElementById('suggestions');
const getWeatherBtn = document.getElementById('getWeather');
const resultDiv = document.getElementById('result');

let selectedCoordinates = null;
let activeSuggestionIndex = -1;
let debounceTimer = null;
let lastQuery = '';
let currentController = null;

const AppConfig = {
    ApiHost: '',
    ApiBasePath: '/api/v0.1'
};

providerList.forEach(p => {
    const option = document.createElement('option');
    option.value = p.value;
    option.textContent = p.text;
    providerSelect.appendChild(option);
});
providerSelect.value = providerList[0].value;

locationInput.addEventListener('input', handleLocationInput);
locationInput.addEventListener('keydown', handleArrowKeys);
suggestionsList.addEventListener('click', e => {
    if (e.target.tagName === 'LI') selectSuggestion(e.target);
});
getWeatherBtn.addEventListener('click', async () => {
    if (!selectedCoordinates) return showWarning('Пожалуйста, выберите локацию из подсказок');
    if (!providerSelect.value) return showWarning('Пожалуйста, выберите провайдера');

    showLoading();
    const response = await getWeather(selectedCoordinates.latitude, selectedCoordinates.longitude, providerSelect.value);
    displayWeather(response);
});

function handleLocationInput() {
    clearTimeout(debounceTimer);
    const query = locationInput.value.trim();
    if (query.length < 2) return hideSuggestions();

    if (query === lastQuery) return;

    debounceTimer = setTimeout(fetchSuggestions, 500, query);
}

async function fetchSuggestions(query) {
    lastQuery = query;

    if (currentController) currentController.abort();
    currentController = new AbortController();
    const signal = currentController.signal;

    try {
        const params = new URLSearchParams({ query, count: 5 });
        const response = await fetch(`${AppConfig.ApiHost}${AppConfig.ApiBasePath}/suggestion?${params}`, { signal });
        if (!response.ok) throw new Error(`Ошибка ${response.status}`);

        const suggestions = await response.json();
        renderSuggestions(suggestions);
    } catch (err) {
        if (err.name !== 'AbortError') console.error(err);
        hideSuggestions();
    } finally {
        currentController = null;
    }
}

function renderSuggestions(items) {
    suggestionsList.innerHTML = '';
    activeSuggestionIndex = -1;

    items.forEach(item => {
        const li = document.createElement('li');
        li.textContent = item.name;
        li.dataset.lat = item.latitude;
        li.dataset.lon = item.longitude;
        suggestionsList.appendChild(li);
    });

    suggestionsList.style.display = items.length ? 'block' : 'none';
}

function hideSuggestions() {
    suggestionsList.style.display = 'none';
    if (currentController) currentController.abort();
    currentController = null;
}

function handleArrowKeys(e) {
    const items = suggestionsList.querySelectorAll('li');
    if (!items.length) return;

    if (e.key === 'ArrowDown') activeSuggestionIndex = (activeSuggestionIndex + 1) % items.length;
    if (e.key === 'ArrowUp') activeSuggestionIndex = (activeSuggestionIndex - 1 + items.length) % items.length;
    if (e.key === 'Enter' && activeSuggestionIndex >= 0) selectSuggestion(items[activeSuggestionIndex]);

    updateActiveSuggestion(items);
    if (['ArrowDown', 'ArrowUp', 'Enter'].includes(e.key)) e.preventDefault();
}

function updateActiveSuggestion(items) {
    items.forEach((item, idx) => item.classList.toggle('active', idx === activeSuggestionIndex));
}

function selectSuggestion(item) {
    locationInput.value = item.textContent;
    selectedCoordinates = { latitude: item.dataset.lat, longitude: item.dataset.lon };
    hideSuggestions();
    getWeather(selectedCoordinates.latitude, selectedCoordinates.longitude, providerSelect.value).then(displayWeather);
}

async function getWeather(lat, lon, provider) {
    try {
        const params = new URLSearchParams({ latitude: lat, longitude: lon });
        if (provider) params.append('weatherProvider', provider);

        const url = `${AppConfig.ApiHost}${AppConfig.ApiBasePath}/weather/current?${params}`;
        const response = await fetch(url);
        const data = await response.json();

        if (!response.ok || data?.error) {
            const err = data.error ?? data;
            return {
                error: true,
                code: err.code ?? 0,
                message: err.message ?? 'Неизвестная ошибка сервера',
                action: err.action ?? null
            };
        }

        return { error: false, data };
    } catch (err) {
        console.error(err);
        return { error: true, code: 0, message: err.message ?? 'Ошибка при запросе к серверу', action: null };
    }
}

function displayWeather(response) {
    if (response.error) {
        let html = `
            <div style="
                background-color: #fff4e5; 
                color: #333; 
                border-left: 4px solid #ffa500; 
                padding: 10px; 
                border-radius: 4px;
                margin-top: 10px;
            ">
                 ${response.message}
        `;
        if (response.action) html += `<div style="margin-top:5px;">Что сделать: ${response.action}</div>`;
        html += `</div>`;
        resultDiv.innerHTML = html;
        return;
    }

    const data = response.data;
    resultDiv.innerHTML = `
        <div class="weather-card">
            <h3>Погода</h3>
            <p>Температура: ${data.temperature ?? 'неизвестно'} °C</p>
            <p>Влажность: ${data.humidity ?? 'неизвестно'}%</p>
            <p>Давление: ${data.pressure ?? 'неизвестно'} мм рт. ст.</p>
        </div>
    `;
}

function showWarning(message) {
    resultDiv.innerHTML = `
        <div style="
            background-color: #fff8dc; 
            color: #555; 
            border-left: 4px solid #ffa500; 
            padding: 10px; 
            border-radius: 4px;
            margin-top: 10px;
        ">
            ${message}
        </div>
    `;
}

function showLoading() {
    resultDiv.innerHTML = '<p>Загрузка...</p>';
}
