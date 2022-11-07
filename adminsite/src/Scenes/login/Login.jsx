import * as React from 'react';
import { Avatar, Button, Box, TextField, CssBaseline, FormControlLabel, Checkbox, Typography, Container } from '@mui/material';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { useNavigate } from 'react-router';
import axios from 'axios';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';

const theme = createTheme();
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

function Login() {
    const navigate = useNavigate()

    const handleSubmit = async (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        await axios.post(`https://localhost:7173/Auth/Login`, 
        {
            userName: data.get('userName'),
            password: data.get('password'),
        }, { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            var jwt = `Bearer ${res.data}`
            document.cookie = `jwt=${jwt}`
            // console.log(document.headers.Authorization);
            navigate("/dashboard");
        }).catch(error => console.log(error))
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
                <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="emuserNameail"
                        label="User Name"
                        name="userName"
                        autoComplete="userName"
                        autoFocus
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
                    />
                    <FormControlLabel
                        control={<Checkbox value="remember" color="primary" />}
                        label="Remember me"
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Sign In
                    </Button>
                </Box>
            </Box>
        </Container>
    </ThemeProvider>
    );
}

export default Login