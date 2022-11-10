import React from "react"
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from "chart.js"
import { Doughnut } from "react-chartjs-2"

ChartJS.register(ArcElement, Tooltip, Legend)

function DoughnutChart({ title, labelsProp, dataProp, colorProp }){
    const options = React.useMemo(() => ({
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: 
            {
                position: "top"
            },
            title: 
            {
                display: true,
                text: title
            }
        }
    }), [])

    const data = {
        labels: labelsProp,
        datasets: [
            {
                label: "# of Votes",
                data: dataProp,
                backgroundColor: colorProp,
                borderColor: colorProp,
                borderWidth: 1
            }
        ]
    }
    

    return (
        <Doughnut options={options} data={data} />
    )
}


export default React.memo(DoughnutChart)
