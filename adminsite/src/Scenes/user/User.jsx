import { Box, useTheme } from '@mui/material'
import { DataGrid, GridToolbar } from '@mui/x-data-grid'
import { tokens } from '../../theme'
import { useEffect, useState, useMemo } from 'react'
import axios from 'axios'
import Header from '../../components/Header'


function User() {
    const [userData, setUserData] = useState([])
    const [pageSize, SetPageSize] = useState(5)
    const theme = useTheme()
    const colors = tokens(theme.palette.mode)

    useEffect(()=>{
        axios.get(`https://localhost:7173/Auth/GetAllUser`)
        .then(res => {
            setUserData(res.data)
        }).catch(error => console.log(error))
    },[])

    const columns = useMemo(() => [
        { field: "id", flex: 1, headerName: "ID" },
        { field: "userName", flex: 1, headerName: "User's Name", flex: 1, cellClassName: "name-column--cell" },
        { field: "email", flex: 1, headerName: "Email" },
        { field: "emailConfirmed", flex: 1, headerName: "Confirmed Email", headerAlign: "center", align: "center", type:"number" },
        { field: "phoneNumber", flex: 1, headerName: "Phone Number", headerAlign: "left", align: "left" },
        { field: "phoneNumberConfirmedConfirmed", flex: 1, headerName: "Confirmed Phone Number", headerAlign: "center", align: "center", type:"number" }
    ], [])

    return (
        <Box m="20px">
            <Header title="User" subTitle="Manage User" />
            <Box m="20px 0 0 0" height="61vh" sx={{
                "& .MuiDataGrid-root": {
                    border: "none",
                },
                "& .MuiDataGrid-cell": {
                    borderBottom: "none",
                },
                "& .MuiDataGrid-columnHeaders": {
                    backgroundColor: colors.blueAccent[700],
                    borderBottom: "none"
                },
                "& .MuiDataGrid-virtualScroller": {
                    backgroundColor: colors.primary[400],
                },
                "& .MuiDataGrid-footerContainer": {
                    borderTop: "none",
                    backgroundColor: colors.blueAccent[700],
                },
                "& .MuiDataGrid-toolbarContainer .MuiButton-text": {
                    color: `${colors.grey[100]} !important`,
                }
            }}>
                <DataGrid 
                    rows={userData==null ? [] : userData} 
                    columns={columns} 
                    columnVisibilityModel={{
                        id:false
                    }}
                    components={{ Toolbar: GridToolbar }}
                    pageSize={pageSize}
                    onPageSizeChange={(newPageSize) => SetPageSize(newPageSize)}
                />
            </Box>
        </Box>
    )
}

export default User