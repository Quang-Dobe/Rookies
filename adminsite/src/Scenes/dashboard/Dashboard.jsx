import { Box } from '@mui/material'
import Header from '../../components/Header'

function Dashboard(){
    return (
        <Box m="20px">
            <Box display="flex" justifyContent="space-between" alignItems="center">
                <Header title="Dashboard" subTitle="Welcome to Admin Site" />
            </Box>
        </Box>
    )
}

export default Dashboard