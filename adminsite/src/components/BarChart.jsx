import React from "react"
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend
} from "chart.js"
import { Bar } from "react-chartjs-2"

ChartJS.register(
    CategoryScale, 
    LinearScale, 
    BarElement, 
    Title, 
    Tooltip, 
    Legend
)


function BarChart({ title, labelsProp, dataProp, label }){
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
                label: label,
                data: dataProp,
                backgroundColor: `#${Math.floor(Math.random()*16777215).toString(16)}`
            }
        ]
    }

    return (
        <Bar options={options} data={data} />
    )
}

export default React.memo(BarChart)
