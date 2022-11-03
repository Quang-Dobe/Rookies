import { Box, Button, TextField } from "@mui/material"
import MenuItem from '@mui/material/MenuItem';
import { Formik } from "formik"
import { useState, useEffect } from "react"
import * as yup from "yup"
import useMediaQuery from "@mui/material/useMediaQuery"
import Header from "../../components/Header"
import axios from 'axios'

const initialValues = {
    productImg : "dsfaasf",
    productName : "",
    description : "",
    categoryId : "",
    price : "",
    quantity: "",
    inventoryNumber: ""
}
const productSchema = yup.object({
    productImg : yup.string().required("required"),
    productName : yup.string().required("required"),
    description : yup.string().required("required"),
    categoryId : yup.string().required("required"),
    price : yup.string().required("required"),
    quantity: yup.string().required("required"),
    inventoryNumber: yup.string().required("required")
})


function ProductCreateForm() {
    const [category, setCategory] = useState([])
    useEffect(() => {
        axios.get(`https://localhost:7173/api/Category`)
        .then(res => {
            setCategory(res.data)
        }).catch(error => console.log(error))
    }, [])
    const categorySelection = category.reduce((init, curValue) => [...init, {value:curValue.id, label:curValue.name}], [])
    
    const isNoneMobile = useMediaQuery("(min-width:600px)");
    const handleFormSubmit = (values) => {
        console.log(values);
    }

    return(
        <Box m="20px">
            <Header title="CREATE PRODUCT" subTitle="Create new product" />
            <Formik
                onSubmit={handleFormSubmit}
                initialValues={initialValues}
                validationSchema={productSchema}
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
                            {/* Product Image */}
                            <TextField 
                                fullWidth
                                variant="filled"
                                type="text"
                                label="Product Image"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                defaultValue={values.productImg}
                                name="Product Img"
                                error={!!touched.productImg && errors.productImg }
                                helperText={touched.productImg && errors.productImg }
                                sx={{ gridColumn: "span 2" }}
                            />

                            {/* Product Name */}
                            <TextField 
                                fullWidth
                                variant="filled"
                                type="text"
                                label="Product Name"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                defaultValue={values.productName}
                                name="Product Name"
                                error={!!touched.productName && errors.productName }
                                helperText={touched.productName && errors.productName }
                                sx={{ gridColumn: "span 2" }}
                            />

                            {/* Product Description */}
                            <TextField 
                                fullWidth
                                variant="filled"
                                type="text"
                                label="Product Description"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                defaultValue={values.description}
                                name="Product Description"
                                error={!!touched.description && errors.description }
                                helperText={touched.description && errors.description }
                                sx={{ gridColumn: "span 4" }}
                            />

                            {/* Category ID */}
                            <TextField 
                                fullWidth
                                select
                                variant="filled"
                                label=""
                                onBlur={handleBlur}
                                onChange={handleChange}
                                defaultValue={values.categoryId}
                                name="Category ID"
                                error={!!touched.categoryId && errors.categoryId }
                                helperText={touched.categoryId && errors.categoryId }
                                sx={{ gridColumn: "span 4" }}
                            >
                                {categorySelection.map((option) => (
                                    <MenuItem key={option.value} value={option.value}>
                                    {option.label}
                                    </MenuItem>
                                ))}
                            </TextField>

                            {/* Product Price */}
                            <TextField 
                                fullWidth
                                variant="filled"
                                type="number"
                                label="Product's Price"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                defaultValue={values.price}
                                name="Product Price"
                                error={!!touched.price && errors.price }
                                helperText={touched.price && errors.price }
                                sx={{ gridColumn: "span 4" }}
                            />

                            {/* Product Quantity */}
                            <TextField 
                                fullWidth
                                variant="filled"
                                type="number"
                                label="Product's Quantity"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                defaultValue={values.quantity}
                                name="Product Quantity"
                                error={!!touched.quantity && errors.quantity }
                                helperText={touched.quantity && errors.quantity }
                                sx={{ gridColumn: "span 4" }}
                            />

                            {/* Product Inventory number */}
                            <TextField 
                                fullWidth
                                variant="filled"
                                type="number"
                                label="Product's Inventory number"
                                onBlur={handleBlur}
                                onChange={handleChange}
                                defaultValue={values.inventoryNumber}
                                name="Product Inventory number"
                                error={!!touched.inventoryNumber && errors.inventoryNumber }
                                helperText={touched.inventoryNumber && errors.inventoryNumber }
                                sx={{ gridColumn: "span 4" }}
                            />
                        </Box>
                    </form>
                )}
            </Formik>
        </Box>
    )
}

export default ProductCreateForm

