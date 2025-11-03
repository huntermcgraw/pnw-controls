import { useEffect, useState } from "react";

function App() {
  const [data, setData] = useState([]);

  useEffect(() => {
    fetch("http://localhost:5141/weatherforecast")
      .then((res) => res.json())
      .then((data) => setData(data))
      .catch((err) => console.error(err));
  }, []);

  return (
  <div className="p-6">
  <h1 className="text-2xl font-bold mb-4">Weather Forecast</h1>

  <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
    {data.map((item, i) => (
      <div key={i} className="bg-white shadow-md rounded-xl p-4">
        <h2 className="text-xl font-semibold">{item.summary}</h2>
        <p className="text-gray-500">
          {new Date(item.date).toLocaleDateString()}
        </p>
        <p className="text-blue-600 text-lg mt-2">
          🌡 {item.temperatureC}°C
        </p>
      </div>
    ))}
  </div>
</div>
  );
}

export default App;
