import React from "react"
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
} from "chart.js"
import { Line } from "react-chartjs-2"

ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
)


function LineChart({ title, labelsProp, dataProp }) {
    var data = {
        labels: labelsProp,
        datasets: [
            {
                label: "Dataset 1",
                data: dataProp,
                borderColor: `#${Math.floor(Math.random()*16777215).toString(16)}`,
                backgroundColor: `#${Math.floor(Math.random()*16777215).toString(16)}`
            }
        ]
    }

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
    
    return (
        <Line options={options} data={data} />
    )
}

export default React.memo(LineChart)