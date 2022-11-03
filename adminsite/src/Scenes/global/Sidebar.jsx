import { useState } from "react"
import { ProSidebar , Menu, MenuItem } from "react-pro-sidebar"

import 'react-pro-sidebar/dist/css/styles.css'
import { Box, IconButton, Typography, useTheme } from "@mui/material"
import { Link } from "react-router-dom"
import { tokens } from "../../theme"
import HomeOutlinedIcon from "@mui/icons-material/HomeOutlined"
// import PeopleOutlinedIcon from "@mui/icons-material/PeopleOutlined"
import ContactsOutlinedIcon from "@mui/icons-material/ContactsOutlined"
// import ReceiptOutlinedIcon from "@mui/icons-material/ReceiptOutlined"
import PersonOutlinedIcon from "@mui/icons-material/PersonOutlined"
// import CalendarTodayOutlinedIcon from "@mui/icons-material/CalendarTodayOutlined"
// import HelpOutlinedIcon from "@mui/icons-material/HelpOutlined"
// import BarChartOutlinedIcon from "@mui/icons-material/BarChartOutlined"
// import PieChartOutlinedIcon from "@mui/icons-material/PieChartOutlined"
// import TimelineOutlinedIcon from "@mui/icons-material/TimelineOutlined"
import MenuOutlinedIcon from "@mui/icons-material/MenuOutlined"
// import MapOutlinedIcon from "@mui/icons-material/MapOutlined"

const Item = ({ title, to, icon, selected, setSelected, isCollapsed }) => {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode)
    return (
        
        <MenuItem active={selected === title} style={{ color:colors.grey[100] }} onClick={() => setSelected(title)} icon={icon}>
            <Link to={to}>
                <Typography>{title}</Typography>
            </Link>
        </MenuItem>
    )
}


function Sidebar() {
    const theme = useTheme();
    const colors = tokens(theme.palette.mode)
    const [isCollapsed, setIsCollapsed] = useState(false);
    const [selected, setSelected] = useState("Dashboard");
    return (
        <Box sx={{
            "& .pro-sidebar-inner": {
                background: `${colors.primary[400]} !important`,
            },
            "& .pro-icon-wrapper": {
                backgroundColor: "transparent !important",
            },
            "& .pro-inner-item": {
                padding: "5px 35px 5px 20px !important",
            },
            "& .pro-inner-item:hover": {
                color: "#868dfb !important",
            },
            "& .pro-menu-item:active": {
                color: "#6870fa !important",
            }
        }}>
            <ProSidebar collapsed={isCollapsed}>
                <Menu style={{
                    backgroundColor: colors.blueAccent[800],
                }}>
                    {/* Menu and Logo */}
                    <MenuItem 
                    onClick={() => setIsCollapsed(!isCollapsed)} 
                    icon={isCollapsed ? <MenuOutlinedIcon /> : undefined } 
                    style={{
                        margin:"10px 0 20px 0", 
                        color: colors.grey[100]
                        }}
                    >
                        {!isCollapsed && 
                        (<Box display="flex" justifyContent="space-between" alignItems="center" ml="15px" >
                            <Typography varriant="h3" color={colors.grey[100]}>
                                ADMINIS
                            </Typography>
                            <IconButton onClick={() => setIsCollapsed(!isCollapsed)}>
                                <MenuOutlinedIcon />
                            </IconButton>
                        </Box>) }
                    </MenuItem>
                    
                    {/* User */}
                    {!isCollapsed && (
                        <Box mb="25px">
                            <Box display="flex" justifyContent="center" alignItems="center">
                                <img alt="profile-user" width="100px" height="100px" src={`../../assets/Avatar_.jpg`} style={{cursor:"pointer", borderRadius:"50%"}} />
                            </Box>

                            <Box textAlign="center">
                                <Typography variant="h2" color={colors.grey[100]} fontWeight="bold" sx={{ m:"10px 0 0 0" }}>Cabe</Typography>
                                <Typography varient="h3" color={colors.greenAccent[500]}>Fancy Admin</Typography>
                            </Box>
                        </Box>
                    )}

                    {/* Menu Items */}
                    <Box paddingLeft={isCollapsed ? undefined : "10%"}>
                        {!isCollapsed ? (<Item 
                            title="Dashboard"
                            to="/"
                            icon={<HomeOutlinedIcon />}
                            selected={selected}
                            setSelected={setSelected}
                        />) : (<Box><IconButton><HomeOutlinedIcon /></IconButton></Box>)}

                        <Typography variant="h6" color={colors.grey[300]} sx={!isCollapsed ? { m:"15px 0 5px 20px" } : { m:"15px 0 5px 10px" }} >Data</Typography>

                        {!isCollapsed ? (<Item 
                            title="Product"
                            to="/product"
                            icon={<ContactsOutlinedIcon />}
                            selected={selected}
                            setSelected={setSelected}
                        />) : (<Box><IconButton><PersonOutlinedIcon /></IconButton></Box>)}

                        <Typography variant="h6" color={colors.grey[300]} sx={!isCollapsed ? { m:"15px 0 5px 20px" } : { m:"15px 0 5px 10px" }}>Pages</Typography>
                        {!isCollapsed ? (<Item 
                            title="Create Product"
                            to="/product/create"
                            icon={<PersonOutlinedIcon />}
                            selected={selected}
                            setSelected={setSelected}
                        />) : (<Box><IconButton><PersonOutlinedIcon /></IconButton></Box>)}
                    </Box>
                </Menu>
            </ProSidebar>
        </Box>
    )
}

export default Sidebar