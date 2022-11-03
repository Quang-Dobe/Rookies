import { DataGrid } from '@mui/x-data-grid';
import { Box, Typography, useTheme, Stack, Button } from '@mui/material'
import { tokens } from '../../theme'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router';
import * as React from 'react';
import Header from '../../components/Header'
import axios from 'axios'
import ProductUpdateForm from './Product_Update';

const columns = [
    { field: "id", flex: 1, headerName: "ID", type: "number" },
    { field: "productImg", flex: 1, headerName: "Product Image" },
    { field: "productName", flex: 1, headerName: "Product's Name", cellClassName: "name-column--cell" },
    { field: "description", flex: 1, headerName: "Description", headerAlign: "left", align: "left" },
    { field: "categoryId", flex: 1, headerName: "Category ID", type: "number" },
    { field: "price", flex: 1, headerName: "Price", type: "number" },
    { field: "quantity", flex: 1, headerName: "Quantity", type: "number" },
    { field: "inventoryNumber", flex: 1, headerName: "Inventory Number", type: "number" },
    { field: "rating", flex: 1, headerName: "Rating", type: "number" },
    { field: "createdDate", flex: 1, headerName: "Created Date"},
    { field: "updatedDate", flex: 1, headerName: "Updated Date"}
]


function Product(){
    const [productData, setProductData] = useState([])
    const [update, setUpdate] = useState(false)
    const theme = useTheme()
    const navigate = useNavigate()
    const colors = tokens(theme.palette.mode)
    var idRow = undefined;
    
    
    console.log("Re-rendered")
    
    const handleUpdateRow = () => {
        setUpdate(!update);
    };
    
    const handleDeleteRow = async () => {
        await axios.delete(`https://localhost:7173/Product/DeleteProduct/${idRow.id}`)

        await axios.get(`https://localhost:7173/Product/GetAllProducts`)
        .then(res => {
            console.log(res.data)
            setProductData(res.data)
        }).catch(error => console.log(error))
    };
    
    const handleAddRow = () => {
        navigate("/product/create")
    };

    useEffect(() => {
        axios.get(`https://localhost:7173/Product/GetAllProducts`)
        .then(res => {
            setProductData(res.data)
        }).catch(error => console.log(error))
    }, [])

    return (
        <Box m="20px">
            <ProductUpdateForm data={idRow} show={update} setShow={setUpdate} />
            <Header title="Product" subTitle="Manage Product" />
            <Stack direction="row" spacing={1}>
                <Button size="small" onClick={handleUpdateRow}>
                    Update a row
                </Button>
                <Button size="small" onClick={handleDeleteRow}>
                    Delete a row
                </Button>
                <Button size="small" onClick={handleAddRow}>
                    Add a row
                </Button>
            </Stack>
            <Box m="40px 0 0 0" height="75vh" sx={{
                "& .MuiDataGrid-root": {
                    border: "none",
                },
                "& .MuiDataGrid-cell": {
                    borderBottom: "none",
                },
                "& .MuiDataGrid-columnHeaders": {
                    backgroundColor: colors.blueAccent[700],
                    borderBottom: "none"
                },
                "& .MuiDataGrid-virtualScroller": {
                    backgroundColor: colors.primary[400],
                },
                "& .MuiDataGrid-footerContainer": {
                    borderTop: "none",
                    backgroundColor: colors.blueAccent[700],
                }
            }}>
                <DataGrid
                    rows={productData==null ? [] : productData} 
                    columns={columns} 
                    onSelectionModelChange={(ids) => {
                        idRow = productData.find(item => item.id == ids);
                    }} 
                />
            </Box>
        </Box>
    )
}

export default Product