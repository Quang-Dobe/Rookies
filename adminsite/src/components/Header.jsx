import { Box, Typography, useTheme } from "@mui/material"
import { tokens } from "../theme"


function Header({ title, subTitle }) {
    const theme = useTheme()
    const colors = tokens(theme.palette.mode)
    return (
        <Box m="30px">
            <Typography variant="h2" color={colors.grey[100]} fontWeight="bold">{title}</Typography>
            <Typography variant="h5" color={colors.greenAccent[400]}>{subTitle}</Typography>
        </Box>
    )
}

export default Header