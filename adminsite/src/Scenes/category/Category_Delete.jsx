import { Box, Modal, Button, useTheme } from '@mui/material'
import { tokens } from '../../theme'

// Style for Modal
const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    border: '2px solid #000',
    boxShadow: 24,
    pt: 2,
    px: 4,
    pb: 3,
};

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

function CategoryDeleteModal(props) {
    const theme = useTheme()
    const colors = tokens(theme.palette.mode)

    return (
        <Modal
            open={props.open}
            onClose={props.handleClose}
            aria-labelledby="delete-modal-title"
            aria-describedby="delete-modal-description"
        >
            <Box sx={{ ...style, width: 400 }} textAlign="center">
                <p id="delete-modal-title">Status</p>
                <Box id="delete-modal-description">
                    {props.status===true ? 
                    <h2 style={{ color:colors.greenAccent[800] }}>Success</h2> : 
                    <h2 style={{ color:colors.redAccent[800] }}>Failed</h2>}
                </Box>
                <Button 
                    onClick={props.handleClose} 
                    color="secondary" 
                    variant="contained" 
                    sx={{ backgroundColor:colors.blueAccent[700] }}
                >
                    Close
                </Button>
            </Box>
        </Modal>
    )
}

export default CategoryDeleteModal
