import { Box, useTheme, IconButton } from '@mui/material'
import { DataGrid, GridToolbar } from '@mui/x-data-grid'
import { tokens } from '../../theme'
import { useEffect, useState } from 'react'
import axios from 'axios'
import Header from '../../components/Header'
import AdminPanelSettingsOutlinedIcon from '@mui/icons-material/AdminPanelSettingsOutlined';
import AccountCircleOutlinedIcon from '@mui/icons-material/AccountCircleOutlined';

// Get Token from Cookie
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

// Parse Token (Not show current Admin Account now)
function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}

function User() {
    const [userData, setUserData] = useState([])
    const [roles, setRoles] = useState([])
    const [pageSize, SetPageSize] = useState(5)
    const theme = useTheme()
    const colors = tokens(theme.palette.mode)

    useEffect(()=>{
        axios.get(`https://localhost:7173/Auth/GetAllUser`,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            setUserData(res.data)
        }).catch(error => console.log(error))

        axios.get(`https://localhost:7173/Auth/GetAllRoles`,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            setRoles(res.data)
        }).catch(error => console.log(error))
    },[])

    const columns = [
        { field: "id", flex: 1, headerName: "ID", hide:true },
        { field: "userName", flex: 1, headerName: "User's Name" },
        { field: "email", flex: 0.8, headerName: "Email" },
        { field: "emailConfirmed", flex: 0.3, headerName: "Confirmed Email", headerAlign: "center", align: "center", type:"number" },
        { field: "phoneNumber", flex: 0.6, headerName: "Phone Number", headerAlign: "left", align: "left" },
        { field: "phoneNumberConfirmedConfirmed", flex: 0.3, headerName: "Confirmed Phone Number" },
        { field: "Role", flex: 1, headerName:"Change role", type: 'actions', headerAlign: "center", align: "center",
            renderCell: ({ row: data }) => {
                // Get Token from Cookie
                var decryptedToken = parseJwt(getCookie('jwt').split(' ')[1])

                const handleOnClick = async (e) => {
                    var changeRole = (data.role==="Admin") ? "User" : "Admin"
                    await axios.post(`https://localhost:7173/Auth/UpdateUserRole?userId=${data.id}`, changeRole,
                    { 
                        headers: {
                            'authorization': getCookie('jwt'),
                            'Accept' : 'application/json',
                            'Content-Type': 'application/json'
                        }
                    })
                    .then(res => console.log(res.data)).catch(error => console.log(error))

                    await axios.get(`https://localhost:7173/Auth/GetAllUser`,
                    { 
                        headers: {
                            'authorization': getCookie('jwt'),
                            'Accept' : 'application/json',
                            'Content-Type': 'application/json'
                        }
                    })
                    .then(res => {
                        setUserData(res.data)
                    }).catch(error => console.log(error))
                }
                return (
                    <Box>
                        {(data.role==="Admin") ? 
                            <IconButton sx={{backgroundColor:colors.greenAccent[500]}} onClick={handleOnClick} disabled={decryptedToken.nameid===data.id}>
                                <AdminPanelSettingsOutlinedIcon />
                                {/* <Typography>Admin</Typography> */}
                            </IconButton>
                        :
                            <IconButton sx={{backgroundColor:colors.redAccent[500]}} onClick={handleOnClick}>
                                <AccountCircleOutlinedIcon />
                                {/* <Typography>User</Typography> */}
                            </IconButton>
                        }
                    </Box>
                );
            },
        }
    ]

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
                    components={{ Toolbar: GridToolbar }}
                    pageSize={pageSize}
                    onPageSizeChange={(newPageSize) => SetPageSize(newPageSize)}
                />
            </Box>
        </Box>
    )
}

export default User