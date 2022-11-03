import { Box, useTheme } from '@mui/material'
import { tokens } from '../../theme'
import { DataGrid } from '@mui/x-data-grid'
import * as React from 'react';
import { useEffect, useState } from 'react'
import Header from '../../components/Header'
import axios from 'axios'

function MockProduct(){
    const theme = useTheme()
    const colors = tokens(theme.palette.mode)
    const [productData, setProductData] = useState([])
    
    const columns = [
        { field: "id", flex: 1, headerName: "ID", type: "number" },
        { field: "productImg", flex: 1, headerName: "Product Image" },
        { field: "productName", flex: 1, headerName: "Product's Name", cellClassName: "name-column--cell" },
        { field: "description", flex: 1, headerName: "Description", headerAlign: "left", align: "left" },
        { field: "productType", flex: 1, headerName: "ProductType", type: "number" },
        { field: "price", flex: 1, headerName: "Price", type: "number" },
        { field: "quantity", flex: 1, headerName: "Quantity", type: "number" },
        { field: "inventoryNumber", flex: 1, headerName: "Inventory Number", type: "number" },
        { field: "rating", flex: 1, headerName: "Rating", type: "number" },
        { field: "createdDate", flex: 1, headerName: "Created Date"},
        { field: "updatedDate", flex: 1, headerName: "Updated Date"}
    ]

    useEffect(() => {
        axios.get(`https://localhost:7173/Product/GetAllProducts`)
        .then(res => {
            setProductData(res.data)
        }).catch(error => console.log(error))
    }, [])

    return (
        <Box m="20px">
            <Header title="Product" subTitle="Manage Product" />
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
                <DataGrid rows={productData} columns={columns} />
            </Box>
        </Box>
    )
}

export default MockProduct