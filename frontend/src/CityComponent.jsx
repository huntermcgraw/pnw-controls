import React, { useState } from 'react';

function CityIDsForm({ onDataSubmit }) {
    const [cityID1, setCityID1] = useState('');
    const [cityID2, setCityID2] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const handleCity1Change = (event) => {
        setCityID1(event.target.value);
    };

    const handleCity2Change = (event) => {
        setCityID2(event.target.value);
    };

    const handleButtonClick = async () => {
        
        setIsLoading(true);

        const dataToSend = {
            cityID1: cityID1,
            cityID2: cityID2
        };

        const backendAPI = "http://localhost:5141/cityinfo";

        try {
            const response = await fetch(backendAPI, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(dataToSend)
            })

            const result = await response.json();

            if (response.ok) {
                onDataSubmit(result);
            } else {
                onDataSubmit({ message: result.message || `API Error: ${response.statusText}` });
                console.error("API failed to process:", result);
            }
        } catch (error) {
            console.error("Error");
            onDataSubmit({ message: "Network error." });
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div>
            <h3>Input Two City IDs to Compare</h3>

            <input
                type="text"
                value={cityID1}
                onChange={handleCity1Change}
                placeholder="Enter City 1 ID"
                style={{ marginRight: '10px', padding: '5px' }}
            />

            <input
                type="text"
                value={cityID2}
                onChange={handleCity2Change}
                placeholder="Enter City 2 ID"
                style={{ marginRight: '10px', padding: '5px' }}
            />

            <button onClick={handleButtonClick}>
                Capture Both Values
            </button>

        </div>
    );
}

export default CityIDsForm;