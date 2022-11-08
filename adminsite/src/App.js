import { ColorModeContext, useMode } from './theme.js'
import { CssBaseline, ThemeProvider } from '@mui/material'
import { Routes, Route } from 'react-router-dom';
import { Navigate } from 'react-router-dom';
import Dashboard from './Scenes/dashboard'
import Topbar from './Scenes/global/Topbar'
import Sidebar from './Scenes/global/Sidebar';
import { User } from './Scenes/user'
import { Product, ProductCreateForm } from './Scenes/product'
import { Category } from './Scenes/category'
import { Login } from './Scenes/login';

// Get Token from Cookie
function getCookie(name) {
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return parts.pop().split(';').shift();
}

function AdminRoute({ CustomComponent }) {
  return (
    getCookie("jwt")==null ?
    <Navigate to="/login" />
    :
    <div className="app">
      <Sidebar />
      <main className="content">
        <Topbar />
        <CustomComponent />
      </main>
    </div>
  )
}

function App() {
  const [theme, colorMode] = useMode();
  return (
    <ColorModeContext.Provider value={colorMode}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
          <Routes>
            <Route path="/" element={<Navigate to="/login" />} />
            <Route path="/login" element={<Login />} />
            <Route path="/dashboard" element={<AdminRoute CustomComponent={Dashboard}/>} />
            <Route path="/user" element={<AdminRoute CustomComponent={User}/>} />
            <Route path="/product" element={<AdminRoute CustomComponent={Product}/>} />
            <Route path="/category" element={<AdminRoute CustomComponent={Category}/>} />
            <Route path="/product/create" element={<AdminRoute CustomComponent={ProductCreateForm}/>} />
          </Routes>
          {/* <div className="app">
            <Sidebar />
            <main className="content">
              <Topbar />
              <Routes>
                <Route path="/dashboard" element={<Dashboard />} />
                <Route path="/user" element={<User />} />
                <Route path="/product" element={<Product />} />
                <Route path="/category" element={<Category />} />
                <Route path="/product/create" element={<ProductCreateForm />} />
              </Routes>
            </main>
          </div> */}
      </ThemeProvider>
    </ColorModeContext.Provider>
  );
}

export default App;
