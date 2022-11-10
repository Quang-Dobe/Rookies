import { Box, useTheme } from '@mui/material'
import { tokens } from '../../theme'
import { useEffect, useState } from 'react'
import { Header, BarChart, DoughnutChart, LineChart } from '../../components'
import axios from 'axios'


// Get Token from Cookie
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

function Dashboard(){
    const [barChart, setBarChart] = useState([])
    const [doughnutChart, setDoughnutChart] = useState([])
    const [lineChart1, setLineChart1] = useState([])
    const [lineChart2, setLineChart2] = useState([])
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);

    useEffect(() => {
        // BarChart
        axios.get(`https://localhost:7173/Dashboard/OrderByOrderAndCart`,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            setBarChart(res.data)
        }).catch(error => console.log(error))

        // DoughnutChart
        axios.get(`https://localhost:7173/Dashboard/TotalOfEachCategory`,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            setDoughnutChart(res.data)
        }).catch(error => console.log(error))
        
        // LineChart 1
        axios.get(`https://localhost:7173/Dashboard/TotalInThreeDays`,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            setLineChart1(res.data)
        }).catch(error => console.log(error))

        // LineChart 2
        axios.get(`https://localhost:7173/Dashboard/OrderInThreeDays`,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            setLineChart2(res.data)
        }).catch(error => console.log(error))
    }, [])
    
    return (
        <Box m="20px">
            <Box display="flex" justifyContent="space-between" alignItems="center">
                <Header title="Dashboard" subTitle="Welcome to Admin Site" />
            </Box>

            {/* GRID & CHARTS */}
            <Box
                height="65vh"
                display="grid"
                gridTemplateColumns="repeat(12, 1fr)"
                gridTemplateRows="repeat(6, 1fr)"
                gridAutoRows="70px"
                gap="10px"
            >
                {/* ROW 1 */}
                {/* BarChart */}
                <Box
                    gridColumn="span 6"
                    gridRow="span 3"
                    backgroundColor={colors.primary[400]}
                    display="flex"
                    alignItems="center"
                    justifyContent="center"
                >
                    <BarChart 
                        title="Product purchase rate of each category" 
                        labelsProp={barChart.reduce((init, current) => [...init, current.categoryName], [])}
                        dataProp={barChart.reduce((init, current) => [...init, current.total], [])}
                    />
                </Box>

                {/* PieChart */}
                <Box
                    gridColumn="span 6"
                    gridRow="span 3"
                    backgroundColor={colors.primary[400]}
                    display="flex"
                    alignItems="center"
                    justifyContent="center"
                >
                    <DoughnutChart 
                        title="Ratio of sales per product type" 
                        labelsProp={doughnutChart.reduce((init, current) => [...init, current.categoryName], [])}
                        dataProp={doughnutChart.reduce((init, current) => [...init, current.total], [])}
                        colorProp={doughnutChart.reduce((init, current) => [...init, `#${Math.floor(Math.random()*16777215).toString(16)}`], [])}
                    />
                </Box>

                {/* ROW 2 */}
                {/* LineChart 1 */}
                <Box
                    gridColumn="span 6"
                    gridRow="span 3"
                    backgroundColor={colors.primary[400]}
                >
                    <LineChart
                        title="Revenue in the last 3 days"
                        labelsProp={["3 days ago", "2 days ago", "Yesterday"]}
                        dataProp={lineChart1}
                    />
                </Box>

                {/* LineChart 2 */}
                <Box
                    gridColumn="span 6"
                    gridRow="span 3"
                    backgroundColor={colors.primary[400]}
                >
                    <LineChart
                        title="Number of orders in the last 3 days"
                        labelsProp={["3 days ago", "2 days ago", "Yesterday"]}
                        dataProp={lineChart2}
                    />
                </Box>
            </Box>
        </Box>
    )
}

export default Dashboard