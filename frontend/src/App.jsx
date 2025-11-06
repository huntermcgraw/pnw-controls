import { useEffect, useState } from "react";
import CityIDsForm from './CityComponent';

function App() {
  const [data, setData] = useState({ name1: '', mil1: {}, name2: '', mil2: {} });

  const handleData = (newData) => {
    if (newData && newData.name1) {
      setData(newData);
    }
  };

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-8">City Data</h1>
      <CityIDsForm onDataSubmit={handleData}/>
      <div className="flex flex-col md:flex-row gap-30">
        {/* City 1 */}
        <div className="flex-1">
          <h2 className="text-2xl font-bold mb-4 whitespace-nowrap overflow-x-auto">
            City 1: <span className="whitespace-nowrap">{data.name1}</span>
          </h2>
          <div className="grid gap-4 sm:grid-cols-1">
            {Object.entries(data.mil1).map(([key, value], i) => (
              <div key={i} className="bg-black shadow-md rounded-xl p-4">
                <h2 className="text-xl font-semibold">{key}</h2>
                <p className="text-gray-500">
                  {value !== null ? value : "null"}
                </p>
              </div>
            ))}
          </div>
        </div>

        {/* City 2 */}
        <div className="flex-1">
          <h2 className="text-2xl font-bold mb-4">
            City 2: {data.name2}
          </h2>
          <div className="grid gap-4 sm:grid-cols-1">
            {Object.entries(data.mil2).map(([key, value], i) => (
              <div key={i} className="bg-black shadow-md rounded-xl p-4">
                <h2 className="text-xl font-semibold">{key}</h2>
                <p className="text-gray-500">
                  {value !== null ? value : "null"}
                </p>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
