import { DataGrid, GridToolbar } from '@mui/x-data-grid';
import { Box, Typography, useTheme, Stack, Button, IconButton } from '@mui/material'
import { tokens } from '../../theme'
import React, { useEffect, useState, useMemo, useRef } from 'react'
import axios from 'axios'
import Header from '../../components/Header'
import CategoryCreateModal from './Category_Create';
import CategoryUpdateModal from './Category_Update'
import CategoryDeleteModal from './Category_Delete';
import UpdateOutlinedIcon from '@mui/icons-material/UpdateOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';

// Code js (After being converted from Typescript)
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

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

function Category(){
    const [categoryData, setCategoryData] = useState([])
    const [pageSize, SetPageSize] = useState(5)
    const [openCreate, setOpenCreate] = useState(false);
    const [openUpdate, setOpenUpdate] = useState(false);
    const [openDelete, setOpenDelete] = useState(false);
    const theme = useTheme()
    const colors = tokens(theme.palette.mode)
    // Get ID of Row that user is selecting
    var idSelectedRow = useRef(-1)
    // [DeleteModal] Call API for deleting Category returns sucessfully or not?
    var isDeleteSuccess = useRef()


    // Handle Open Create Modal
    const handleOpenCreateModal = () => {
        setOpenCreate(true);
    };
    const handleCloseCreateModal = () => {
        setOpenCreate(false);
    };

    // Handle Open Update Modal
    const handleOpenUpdateModal = () => {
        setOpenUpdate(true);
    };
    const handleCloseUpdateModal = () => {
        setOpenUpdate(false);
    };

    // Handle Open Delete Modal
    const handleOpenDeleteModal = () => {
        setOpenDelete(true);
    };
    const handleCloseDeleteModal = () => {
        setOpenDelete(false);
    };

    // Handle Action (CRUD Category)
    const handleAddRow = () => {
        handleOpenCreateModal()
    }
    const handleUpdateRow = (data) => {
        idSelectedRow.current = data.id
        handleOpenUpdateModal()
    }
    const handleDeleteRow = async (data) => {
        // Call API for deleting Category
        await axios.delete(`https://localhost:7173/api/Category/${data.id}`,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            isDeleteSuccess.current=true;
        }).catch(error => isDeleteSuccess.current=false)

        // Open Modal to inform whether Delete action is sucessful or not
        handleOpenDeleteModal()
        
        await axios.get(`https://localhost:7173/api/Category`,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            setCategoryData(res.data)
        }).catch(error => console.log(error))
    }

    // Columns definition for DataGrid
    const columns = useMemo(() => [
        { field: "id", flex: 1, headerName: "ID", type: "number", headerAlign: "left", align: "left" },
        { field: "name", flex: 4, headerName: "Category's Name", headerAlign: "left", align: "left" },
        { field: "description", flex: 6, headerName: "Description", headerAlign: "left", align: "left" },
        { field: "Action", flex: 4, headerName: "Action", headerAlign: "center", align: "center",
            renderCell: ({ row:data }) => {
                return (
                    <Box>
                        <IconButton onClick={(e) => handleUpdateRow(data)}>
                            <UpdateOutlinedIcon />
                            <Typography>Update</Typography>
                        </IconButton>
                        <IconButton onClick={(e) => handleDeleteRow(data)}>
                            <DeleteOutlineOutlinedIcon />
                            <Typography>Delete</Typography>
                        </IconButton>
                    </Box>
                );
            },
        }
    ], []) 

    // Get All category Information when Component is mounted
    useEffect(() => {
        axios.get(`https://localhost:7173/api/Category/Admin`,
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            setCategoryData(res.data)
        }).catch(error => console.log(error))
    }, [])

    return (
        <React.Fragment>
            <Box m="20px">
                <Header title="Category" subTitle="Manage category" />
                <Stack direction="row" spacing={1}>
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
                        rows={categoryData==null ? [] : categoryData} 
                        columns={columns} 
                        components={{ Toolbar: GridToolbar }}
                        pageSize={pageSize}
                        rowsPerPageOptions={[5, 10, 20]}
                        onPageSizeChange={(newPageSize) => SetPageSize(newPageSize)}
                    />
                </Box>
            </Box>
            {/* Create-modal */}
            <CategoryCreateModal open={openCreate} handleClose={handleCloseCreateModal} setCategoryData={setCategoryData} />
            {/* Update-modal */}
            <CategoryUpdateModal open={openUpdate} handleClose={handleCloseUpdateModal} data={idSelectedRow.current===-1 ? null : categoryData.find(item => item.id===idSelectedRow.current)} setCategoryData={setCategoryData} />
            {/* Delete-modal */}
            <CategoryDeleteModal open={openDelete} handleClose={handleCloseDeleteModal} status={isDeleteSuccess.current} />
        </React.Fragment>
    )
}

export default Category