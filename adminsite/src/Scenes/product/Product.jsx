import { DataGrid, GridToolbar } from '@mui/x-data-grid';
import { Box, Typography, useTheme, Stack, Button, IconButton } from '@mui/material'
import { tokens } from '../../theme'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router';
import * as React from 'react';
import Header from '../../components/Header'
import axios from 'axios'
import UpdateOutlinedIcon from '@mui/icons-material/UpdateOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';


var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};


function Product(){
    const [productData, setProductData] = useState([])
    const [pageSize, SetPageSize] = useState(5)
    const navigate = useNavigate()
    const theme = useTheme()
    const colors = tokens(theme.palette.mode)
    var afterUpdated = undefined
    var afterUpdatedList = []

    const handleUpdateMultiRow = async () => {
        if (afterUpdatedList.length!=0){
            await axios.post(`https://localhost:7173/Product/UpdateMultiProduct/`, afterUpdatedList)
            .then(res => console.log(res.data)).catch(error => console.log(error))

            document.querySelectorAll(`div.MuiDataGrid-cell[data-field="Action"] button:first-child`).forEach(item => {
                item.classList.add("Mui-disabled");
                item.disabled = true;
            })

            await axios.get(`https://localhost:7173/Product/GetAllProducts`)
            .then(res => {
                setProductData(res.data)
            }).catch(error => console.log(error))
        }
    }

    const handleDeleteMultiRow = async () => {
        var listId = afterUpdatedList.reduce((init, current) => [...init, current], [])
        if (listId.length!=0){
            await axios.delete(`https://localhost:7173/Product/DeleteMultiProduct`, listId)
            .then(res => console.log(res.data)).catch(error => console.log(error))

            await axios.get(`https://localhost:7173/Product/GetAllProducts`)
            .then(res => setProductData(res.data)).catch(error => console.log(error))
        }
    }

    const handleUpdateRow = async () => {
        await axios.post(`https://localhost:7173/Product/UpdateProduct/${afterUpdated.id}`, afterUpdated)
        .then(res => {
            console.log(res.data)
        }).catch(error => console.log(error))

        document.querySelectorAll(`div.MuiDataGrid-cell[data-field="Action"] button:first-child`).forEach(item => {
            item.classList.add("Mui-disabled");
            item.disabled = true;
        })

        await axios.get(`https://localhost:7173/Product/GetAllProducts`)
        .then(res => {
            setProductData(res.data)
        }).catch(error => console.log(error))
    };

    const handleDeleteRow = async (data) => {
        await axios.delete(`https://localhost:7173/Product/DeleteProduct/${data.id}`)

        await axios.get(`https://localhost:7173/Product/GetAllProducts`)
        .then(res => setProductData(res.data)).catch(error => console.log(error))
    };

    const handleAddRow = () => {
        navigate("/product/create")
    };

    const columns = [
        { field: "id", flex: 0.3, headerName: "ID", type: "number", headerAlign: "left", align: "left" },
        { field: "productImg", flex: 1, editable:true, hide:true, headerName: "Product Image", headerAlign: "left", align: "left" },
        { field: "productName", flex: 1, editable:true, headerName: "Product's Name", cellClassName: "name-column--cell", 
            preProcessEditCellProps: function (params) {
                var hasError = params.props.value.length < 1;
                return __assign(__assign({}, params.props), { error: hasError });
            } 
        },
        { field: "description", flex: 1, editable:true, hide:true, headerName: "Description", headerAlign: "left", align: "left",
            preProcessEditCellProps: function (params) {
                var hasError = params.props.value.length < 1;
                return __assign(__assign({}, params.props), { error: hasError });
            } 
        },
        { field: "categoryId", flex: 0.5, editable:true, headerName: "Category ID", type: "number" },
        { field: "price", flex: 0.5, editable:true, headerName: "Price", type: "number",
            preProcessEditCellProps: function (params) {
                var hasError = params.props.value <= 0;
                return __assign(__assign({}, params.props), { error: hasError });
            } 
        },
        { field: "quantity", flex: 0.5, editable:true, hide:true, headerName: "Quantity", type: "number",
            preProcessEditCellProps: function (params) {
                var hasError = params.props.value <= 0;
                return __assign(__assign({}, params.props), { error: hasError });
            } 
        },
        { field: "inventoryNumber", flex: 0.5, editable:true, headerName: "Inventory Number", type: "number",
            preProcessEditCellProps: function (params) {
                var hasError = params.props.value <= 0;
                return __assign(__assign({}, params.props), { error: hasError });
            } 
        },
        { field: "rating", flex: 0.5, headerName: "Rating", type: "number" },
        { field: "createdDate", flex: 1, hide:true, headerName: "Created Date"},
        { field: "updatedDate", flex: 1, hide:true, headerName: "Updated Date"},
        { field: "Action", flex: 1, headerName: "Action", headerAlign: "center", align: "center",
            renderCell: ({ row: data }) => {
                return (
                    <Box>
                        <IconButton disabled>
                            <UpdateOutlinedIcon />
                            <Typography>Update</Typography>
                        </IconButton>
                        <IconButton onClick={() => handleDeleteRow(data)}>
                            <DeleteOutlineOutlinedIcon />
                            <Typography>Delete</Typography>
                        </IconButton>
                    </Box>
                );
            },
        }
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
            <Stack direction="row" spacing={1}>
                <Button size="small" onClick={(e) => {handleUpdateMultiRow()}} sx={{color:colors.blueAccent[100], backgroundColor:colors.blueAccent[700]}} secondary>
                    Update multi rows
                </Button>
                {/* <Button size="small" onClick={() => {handleDeleteMultiRow()}} sx={{color:colors.blueAccent[100], backgroundColor:colors.blueAccent[700]}}>
                    Delete multi rows
                </Button> */}
                <Button size="small" onClick={handleAddRow} sx={{color:colors.blueAccent[100], backgroundColor:colors.blueAccent[700]}}>
                    Add new row
                </Button>
            </Stack>
            <Box m="20px 0 0 0" height="61vh" sx={{
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
                },
                "& .MuiDataGrid-toolbarContainer .MuiButton-text": {
                    color: `${colors.grey[100]} !important`,
                }
            }}>
                <DataGrid
                    rows={productData==null ? [] : productData} 
                    columns={columns} 
                    components={{ Toolbar: GridToolbar }}
                    onCellEditCommit={(params) => {
                        // beforeUpdated = {...params.row};
                        afterUpdated = {...params.row};
                        afterUpdated[params.field] = params.value
                        afterUpdatedList.unshift(afterUpdated)
                    }}
                    componentsProps={{
                        row:{
                            onInput: (event) => {
                                var updateButton = event.target.parentElement.parentElement.parentElement.querySelector(`div[data-field="Action"] div button`);
                                updateButton.disabled = false;
                                updateButton.classList.remove("Mui-disabled")
                                updateButton.onclick = function(e) {
                                    handleUpdateRow()
                                }
                            }
                        }
                    }}
                    pageSize={pageSize}
                    onPageSizeChange={(newPageSize) => SetPageSize(newPageSize)}
                />
            </Box>
        </Box>
    )
}

export default Product