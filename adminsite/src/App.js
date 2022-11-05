import { ColorModeContext, useMode } from './theme.js'
import { CssBaseline, ThemeProvider } from '@mui/material'
import { Routes, Route } from 'react-router-dom';
import Dashboard from './Scenes/dashboard'
import Topbar from './Scenes/global/Topbar'
import Sidebar from './Scenes/global/Sidebar';
import { User } from './Scenes/user'
import { Product, ProductCreateForm } from './Scenes/product'
import { Category } from './Scenes/category'


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
                <Route path="/user" element={<User />} />
                <Route path="/product" element={<Product />} />
                <Route path="/category" element={<Category />} />
                <Route path="/product/create" element={<ProductCreateForm />} />
              </Routes>
            </main>
          </div>
      </ThemeProvider>
    </ColorModeContext.Provider>
  );
}

export default App;
