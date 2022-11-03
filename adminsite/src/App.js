import { ColorModeContext, useMode } from './theme.js'
import { CssBaseline, ThemeProvider } from '@mui/material'
import { Routes, Route } from 'react-router-dom';
import Topbar from './Scenes/global/Topbar'
import Dashboard from './Scenes/dashboard'
// import User from './Scenes/user'
import Product from './Scenes/product'
import ProductCreateForm from './Scenes/product/Product_Create.jsx';
import Sidebar from './Scenes/global/Sidebar';


function App() {
  const [theme, colorMode] = useMode();
  return (
    <ColorModeContext.Provider value={colorMode}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
          <div className="app">
            <Sidebar />
            <main className="content">
              <Topbar />
              <Routes>
                <Route path="/" element={<Dashboard />} />
                {/* <Route path="/user" element={<User />} /> */}
                <Route path="/product" element={<Product />} />
                <Route path="/product/create" element={<ProductCreateForm />} />
              </Routes>
            </main>
          </div>
      </ThemeProvider>
    </ColorModeContext.Provider>
  );
}

export default App;
