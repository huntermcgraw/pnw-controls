function MilButton() {
    const handleClick = () => {
        alert("Test");
    };
    return (
        <button onClick={handleClick}>
            Compare Mil
        </button>
    );
} 

export default MilButton;