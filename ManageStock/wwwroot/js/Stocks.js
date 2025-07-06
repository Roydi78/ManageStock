fetch('/Dashboard/GetStockChart')
    .then(response => response.json())
    .then(data => {
        new Chart(document.getElementById('stockChart'), {
            type: 'bar',
            data: data,
            options: {
                responsive: true,
                plugins: {
                    legend: { display: false } // facultatif si tu veux aussi cacher la légende
                }
            }
        });
    });
