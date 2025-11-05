import React, { useState } from 'react';

function DualInputForm() {
    const [cityID1, setCityID1] = useState('');
    const [cityID2, setCityID2] = useState('');
    
    const [capturedData, setCapturedData] = useState(null);

    const handleCity1Change = (event) => {
        setCityID1(event.target.value);
    };

    const handleCity2Change = (event) => {
        setCityID2(event.target.value);
    };

    const handleButtonClick = () => {
        setCapturedData({ city1: cityID1, city2: cityID2 });
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

export default DualInputForm;