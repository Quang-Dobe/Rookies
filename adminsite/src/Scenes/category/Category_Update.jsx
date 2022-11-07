import { Box, Modal, Button, TextField, useTheme } from '@mui/material'
import { useState, useRef } from 'react'
import { Formik } from "formik"
import { tokens } from '../../theme'
import useMediaQuery from "@mui/material/useMediaQuery"
import axios from 'axios'
import * as Yup from 'yup';

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

function CategoryUpdateModal(props) {
    const [open, setOpen] = useState(false);
    const theme = useTheme()
    const colors = tokens(theme.palette.mode)
    const isNoneMobile = useMediaQuery("(min-width:600px)");
    var status = useRef()

    // Open / Close Modal inform that Update action is sucessful or not
    const handleOpen = () => {
        setOpen(true);
    };
    const handleClose = () => {
        setOpen(false);
    };

    // Initial Value for Formik form
    const initialValues = {
        id : props.data?.id,
        name : props.data?.name,
        description : props.data?.description
    }
    // ValidationSchema for Formik form
    const productSchema = Yup.object().shape({
        name : Yup.string().required("required")
    })

    // HandleSubmit Formik form
    const handleFormSubmit = async (values) => {
        await axios.post(`https://localhost:7173/api/Category/Update`, values,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            status.current=true;
        }).catch(error => status.current=false)
        handleOpen()
        
        await axios.get(`https://localhost:7173/api/Category/Admin`,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            props.setCategoryData(res.data)
        }).catch(error => console.log(error))
    }

    return (
        <Modal
            open={props.open}
            onClose={props.handleClose}
            aria-labelledby="parent-modal-title"
            aria-describedby="parent-modal-description"
        >
            <Box sx={{ ...style, width: 400 }}>
                <h2 id="parent-modal-title">Update Category Form</h2>
                <Box id="parent-modal-description">
                    <Formik
                        validationSchema={productSchema}
                        initialValues={initialValues}
                        onSubmit={handleFormSubmit}
                    >
                        {({ values, errors, touched, handleBlur, handleChange, handleSubmit }) => (
                            <form onSubmit={handleSubmit}>
                                <Box
                                    display="grid"
                                    gap="30px"
                                    gridTemplateColumns="repeat(4, minmax(0, 1fr))"
                                    sx={{
                                        "& > div": { gridColumn: isNoneMobile ? undefined : "span 4" },
                                    }}
                                >
                                    {/* Category ID */}
                                    <TextField 
                                        fullWidth
                                        variant="filled"
                                        type="text"
                                        label="Category ID"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        defaultValue={values.id}
                                        inputProps={{ readOnly: true, }}
                                        name="id"
                                        sx={{ gridColumn: "span 1" }}
                                    />

                                    {/* Category Name */}
                                    <TextField 
                                        fullWidth
                                        variant="filled"
                                        type="text"
                                        label="Category Name"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        defaultValue={values.name}
                                        name="name"
                                        error={!!touched.name && !!errors.name }
                                        helperText={touched.name && errors.name }
                                        sx={{ gridColumn: "span 3" }}
                                    />

                                    {/* Category Description */}
                                    <TextField 
                                        fullWidth
                                        variant="filled"
                                        type="text"
                                        label="Category Description"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        defaultValue={values.description}
                                        name="description"
                                        error={!!touched.description && !!errors.description }
                                        helperText={touched.description && errors.description }
                                        sx={{ gridColumn: "span 4" }}
                                    />
                                </Box>
                                <Box display="flex" justifyContent="end" mt="20px">
                                    <Button 
                                        type="submit" 
                                        color="secondary" 
                                        variant="contained" 
                                        sx={{ backgroundColor:colors.blueAccent[700] }}
                                    >
                                        Update Category
                                    </Button>
                                    <Modal
                                        hideBackdrop
                                        open={open}
                                        onClose={handleClose}
                                        aria-labelledby="child-modal-title"
                                        aria-describedby="child-modal-description"
                                    >
                                        <Box sx={{ ...style, width: 200 }} textAlign="center">
                                            <p id="child-modal-title">Status</p>
                                            <Box id="child-modal-description">
                                                {status.current==true ? 
                                                <h2 style={{ color:colors.greenAccent[800] }}>Success</h2> : 
                                                <h2 style={{ color:colors.redAccent[800] }}>Failed</h2>}
                                            </Box>
                                            <Button 
                                                onClick={handleClose} 
                                                color="secondary" 
                                                variant="contained" 
                                                sx={{ backgroundColor:colors.blueAccent[700] }}
                                            >
                                                Close
                                            </Button>
                                        </Box>
                                    </Modal>
                                </Box>
                            </form>
                        )}
                    </Formik>
                </Box>
            </Box>
        </Modal>
    )
}

export default CategoryUpdateModal