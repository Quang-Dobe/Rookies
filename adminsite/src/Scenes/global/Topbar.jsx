import { Box, IconButton, useTheme } from '@mui/material'
import { useContext } from 'react'
import { ColorModeContext, tokens } from '../../theme'
import { useNavigate } from 'react-router'
import InputBase from '@mui/material/InputBase'
import LightModeOutlinedIcon from '@mui/icons-material/LightModeOutlined'
import DarkModeOutlinedIcon from '@mui/icons-material/DarkModeOutlined'
import SearchIcon from '@mui/icons-material/Search'
import LogoutIcon from '@mui/icons-material/Logout'
import axios from 'axios'
// import NotificationsOutlinedIcon from '@mui/icons-material/NotificationsOutlined'
// import SettingsOutlinedIcon from '@mui/icons-material/SettingsOutlined'
// import PersonOutlinedIcon from '@mui/icons-material/PersonOutlined'

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}
function deleteCookie(name) {
    document.cookie = name +'=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

function Topbar() {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode);
    const colorMode = useContext(ColorModeContext);
    const navigate = useNavigate();

    return (
        <Box display="flex" justifyContent="space-between" p={2}>
            {/* Search Bar */}
            <Box display="flex" backgroundColor={colors.primary[400]} borderRadius="3px">
                <InputBase sx={{ ml:2, flex:1 }} placeholder="Search" />
                <IconButton>
                    <SearchIcon />
                </IconButton>
            </Box>
            {/* Icon Buttons */}
            <Box display="flex">
                <IconButton onClick={colorMode.toggleColorMode}>
                    {theme.palette.mode === 'dark' ? (<DarkModeOutlinedIcon />) : (<LightModeOutlinedIcon />)}
                </IconButton>
                {/* <IconButton>
                    <NotificationsOutlinedIcon />
                </IconButton>
                <IconButton>
                    <SettingsOutlinedIcon />
                </IconButton>
                <IconButton>
                    <PersonOutlinedIcon />
                </IconButton> */}
                <IconButton onClick={async () => {
                        await axios.post(`https://localhost:7173/Auth/LogOut`, {}, 
                        { 
                            headers: {
                                'authorization': getCookie('jwt'),
                                'Accept' : 'application/json',
                                'Content-Type': 'application/json'
                            }
                        })
                        .then(res => {
                            deleteCookie("jwt")
                        }).catch(error => console.log(error))

                        navigate("/login")
                    }}>
                    <LogoutIcon />
                </IconButton>
            </Box>
        </Box>
    )
}

export default Topbar