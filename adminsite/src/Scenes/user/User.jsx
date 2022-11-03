import { Box, IconButton, Typography, useTheme } from '@mui/material'
import { DataGrid } from '@mui/x-data-grid'
import { tokens } from '../../theme'
import { UserData } from "../../mockData/UserData"
import Header from '../../components/Header'


function User() {
    const theme = useTheme()
    const colors = tokens(theme.palette.mode)

    const columns = [
        { field: "Id", flex: 1, headerName: "ID" },
        { filed: "Email", flex: 1, headerName: "Email" },
        { field: "UserName", flex: 1, headerName: "User's Name", flex: 1, cellClassName: "name-column--cell" },
        { field: "PhoneNumber", flex: 1, headerName: "Phone Number", headerAlign: "left", align: "left" }
    ]

    return (
        <Box m="20px">
            <Header title="User" subTitle="Manage User" />
            <Box m="40px 0 0 0" height="75vh">
                <DataGrid rows={UserData} columns={columns} />
            </Box>
        </Box>
    )
}

export default User