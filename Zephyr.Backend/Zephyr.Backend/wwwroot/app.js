const providerSelect = document.getElementById('provider');
const locationInput = document.getElementById('location');
const suggestionsList = document.getElementById('suggestions');
const getWeatherBtn = document.getElementById('getWeather');
const resultDiv = document.getElementById('result');

let selectedCoordinates = null;
let activeSuggestionIndex = -1;

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

providerSelect.value = providerList[0].value

let debounceTimer;
let lastQuery = '';
let currentController = null;

locationInput.addEventListener('input', () => {
    clearTimeout(debounceTimer);
    const query = locationInput.value.trim();

    if (query.length < 2) {
        suggestionsList.style.display = 'none';
        if (currentController) {
            currentController.abort();
            currentController = null;
        }
        return;
    }

    if (query === lastQuery) return;

    debounceTimer = setTimeout(async () => {
        lastQuery = query;

        // Отменяем предыдущий запрос
        if (currentController) {
            currentController.abort();
        }
        currentController = new AbortController();
        const signal = currentController.signal;

        try {
            const params = new URLSearchParams({Query: query, Count: 5});
            const response = await fetch(
                `${AppConfig.ApiHost}${AppConfig.ApiBasePath}/suggestion?${params.toString()}`,
                {signal}
            );
            if (!response.ok) throw new Error(`Ошибка ${response.status}`);
            const suggestions = await response.json();

            suggestionsList.innerHTML = '';
            activeSuggestionIndex = -1;

            suggestions.forEach(item => {
                const li = document.createElement('li');
                li.textContent = item.name;
                li.dataset.lat = item.latitude;
                li.dataset.lon = item.longitude;
                suggestionsList.appendChild(li);
            });

            suggestionsList.style.display = suggestions.length ? 'block' : 'none';
        } catch (err) {
            if (err.name !== 'AbortError') {
                console.error(err);
                suggestionsList.style.display = 'none';
            }
        } finally {
            currentController = null;
        }
    }, 500);
});

locationInput.addEventListener('keydown', (e) => {
    const items = suggestionsList.querySelectorAll('li');
    if (!items.length) return;

    if (e.key === 'ArrowDown') {
        activeSuggestionIndex = (activeSuggestionIndex + 1) % items.length;
        updateActiveSuggestion(items);
        e.preventDefault();
    } else if (e.key === 'ArrowUp') {
        activeSuggestionIndex = (activeSuggestionIndex - 1 + items.length) % items.length;
        updateActiveSuggestion(items);
        e.preventDefault();
    } else if (e.key === 'Enter') {
        if (activeSuggestionIndex >= 0) {
            selectSuggestion(items[activeSuggestionIndex]);
            e.preventDefault();
        }
    }
});

function updateActiveSuggestion(items) {
    items.forEach((item, idx) => item.classList.toggle('active', idx === activeSuggestionIndex));
}

suggestionsList.addEventListener('click', (e) => {
    if (e.target.tagName === 'LI') selectSuggestion(e.target);
});

function selectSuggestion(item) {
    locationInput.value = item.textContent;
    selectedCoordinates = {
        latitude: item.dataset.lat,
        longitude: item.dataset.lon
    };
    suggestionsList.style.display = 'none';

    getWeather(selectedCoordinates.latitude, selectedCoordinates.longitude, providerSelect.value).then(displayWeather);
}

async function getWeather(lat, lon, provider) {
    try {
        const params = new URLSearchParams({Latitude: lat, Longitude: lon});
        if (provider) params.append('WeatherProvider', provider);

        const url = `${AppConfig.ApiHost}${AppConfig.ApiBasePath}/weather/current?${params.toString()}`;
        const response = await fetch(url);
        if (!response.ok) throw new Error(`Ошибка ${response.status}`);
        const data = await response.json();
        return data;
    } catch (err) {
        console.error(err);
        return {error: err.message};
    }
}

getWeatherBtn.addEventListener('click', async () => {
    if (!selectedCoordinates) {
        alert('Выберите локацию из подсказок');
        return;
    }

    const provider = providerSelect.value;
    if (!provider) {
        alert('Выберите провайдера');
        return;
    }

    resultDiv.innerHTML = '<p>Загрузка...</p>';

    const data = await getWeather(selectedCoordinates.latitude, selectedCoordinates.longitude, provider);
    displayWeather(data);
});

function displayWeather(data) {
    if (data.error) {
        resultDiv.innerHTML = `<p style="color:red;">Ошибка: ${data.error}</p>`;
        return;
    }

    resultDiv.innerHTML = `
        <div class="weather-card">
            <h3>Погода</h3>
            <p>Температура: ${data.temperature ?? 'неизвестно'} °C</p>
            <p>Влажность: ${data.humidity ?? 'неизвестно'}%</p>
            <p>Давление: ${data.pressure ?? 'неизвестно'} мм рт. ст.</p>
        </div>
    `;
}
