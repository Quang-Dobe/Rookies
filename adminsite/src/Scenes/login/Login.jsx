import * as React from 'react';
import { Avatar, Button, Box, TextField, CssBaseline, Typography, Container } from '@mui/material';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { useNavigate } from 'react-router';
import { Formik } from "formik"
import * as Yup from 'yup';
import useMediaQuery from "@mui/material/useMediaQuery"
import axios from 'axios';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';

const theme = createTheme();

// Get Token from Cookie
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

// InitialValue and Schema for Formik form
const initialValues = {
    userName : "",
    password : ""
}
const loginSchema = Yup.object().shape({
    userName : Yup.string().required("required"),
    password : Yup.string().required("required"),
})

function Login() {
    const navigate = useNavigate()
    const isNoneMobile = useMediaQuery("(min-width:600px)");

    const handleFormSubmit = async (values) => {
        await axios.post(`https://localhost:7173/Auth/Login`, values, { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            var jwt = `Bearer ${res.data}`
            document.cookie = `jwt=${jwt}`
            navigate("/dashboard");
        }).catch(error => {
            alert("Login failed")
        })
    };

    return (
    <ThemeProvider theme={theme}>
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Log in
                </Typography>
                <Box sx={{ mt: 1 }}>
                    <Formik
                        validationSchema={loginSchema}
                        initialValues={initialValues}
                        onSubmit={handleFormSubmit}
                    >
                        {({ values, errors, touched, handleBlur, handleChange, handleSubmit }) => (
                            <form onSubmit={handleSubmit}>
                                <Box
                                    display="grid"
                                    gap="30px"
                                    gridTemplateColumns="repeat(5, minmax(0, 5fr))"
                                    sx={{
                                        "& > div": { gridColumn: isNoneMobile ? undefined : "span 5" },
                                    }}
                                >
                                    <TextField
                                        margin="normal"
                                        required
                                        fullWidth
                                        id="emuserNameail"
                                        label="User Name"
                                        name="userName"
                                        autoComplete="userName"
                                        autoFocus
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        defaultValue={values.userName}
                                        error={ !!touched.userName && !!errors.userName }
                                        helperText={ touched.userName && errors.userName }
                                        sx={{ gridColumn: "span 5" }}
                                    />
                                    <TextField
                                        margin="normal"
                                        required
                                        fullWidth
                                        name="password"
                                        label="Password"
                                        type="password"
                                        id="password"
                                        autoComplete="current-password"
                                        onBlur={handleBlur}
                                        onChange={handleChange}
                                        defaultValue={values.password}
                                        error={ !!touched.password && !!errors.password }
                                        helperText={ touched.password && errors.password }
                                        sx={{ gridColumn: "span 5" }}
                                    />
                                </Box>
                                <Box display="flex" justifyContent="end" mt="20px">
                                    <Button type="submit" color="secondary" variant="contained">
                                        Log in
                                    </Button>
                                </Box>
                            </form>
                        )}
                    </Formik>
                </Box>
            </Box>
        </Container>
    </ThemeProvider>
    );
}

export default Login