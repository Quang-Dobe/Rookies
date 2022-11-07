import { Box, Button, TextField, MenuItem } from "@mui/material"
import { useState, useEffect } from "react"
import { useNavigate } from "react-router";
import { Formik } from "formik"
import * as Yup from 'yup';
import useMediaQuery from "@mui/material/useMediaQuery"
import Header from "../../components/Header"
import axios from 'axios'

const initialValues = {
    productImg : "",
    productName : "",
    description : "",
    categoryId : "",
    price : "",
    quantity: "",
    inventoryNumber: ""
}
const productSchema = Yup.object().shape({
    productImg : Yup.string().required("required"),
    productName : Yup.string().required("required"),
    description : Yup.string().required("required"),
    categoryId : Yup.string().required("required"),
    price : Yup.string().required("required"),
    quantity: Yup.string().required("required"),
    inventoryNumber: Yup.string().required("required")
})
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}

var today = new Date();
var date = today.getFullYear()+'-'+(today.getMonth()+1)+'-'+today.getDate();
var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
var dateTime = date+'T'+time;

function ProductCreateForm() {
    const [category, setCategory] = useState([])
    const isNoneMobile = useMediaQuery("(min-width:600px)");
    const navigate = useNavigate()

    // Call API to get All Product Information
    useEffect(() => {
        axios.get(`https://localhost:7173/api/Category`)
        .then(res => {
            setCategory(res.data)
        }).catch(error => console.log(error))
    }, [])

    const categorySelection = category.reduce((init, curValue) => [...init, {value:curValue.id, label:curValue.name}], [])

    const handleFormSubmit = (values) => {
        axios.post(`https://localhost:7173/Product/CreateNewProduct`, {...values, "id":0, "rating": 0, "createdDate": "2022-11-03T15:02:27.5314911", "updatedDate": "2022-11-03T15:02:27.5314911" }, 
        { 
            headers: {
                'authorization': getCookie('jwt'),
                'Accept' : 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(res => {
            setCategory(res.data)
        }).catch(error => console.log(error))
        navigate("/product");
    }

    return(
        <Box m="20px">
            <Header title="CREATE PRODUCT" subTitle="Create new product" />
            <Formik
                validationSchema={productSchema}
                initialValues={initialValues}
                onSubmit={handleFormSubmit}
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
                                defaultValue={values.productName}
                                name="productImg"
                                error={!!touched.productImg && !!errors.productImg}
                                helperText={touched.productImg && errors.productImg}
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
                                name="productName"
                                error={!!touched.productName && !!errors.productName }
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
                                name="description"
                                error={!!touched.description && !!errors.description }
                                helperText={touched.description && errors.description }
                                sx={{ gridColumn: "span 3" }}
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
                                name="categoryId"
                                error={!!touched.categoryId && !!errors.categoryId }
                                helperText={touched.categoryId && errors.categoryId }
                                sx={{ gridColumn: "span 1" }}
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
                                name="price"
                                error={!!touched.price && !!errors.price }
                                helperText={touched.price && errors.price }
                                sx={{ gridColumn: "span 1" }}
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
                                name="quantity"
                                error={!!touched.quantity && !!errors.quantity }
                                helperText={touched.quantity && errors.quantity }
                                sx={{ gridColumn: "span 1" }}
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
                                name="inventoryNumber"
                                error={!!touched.inventoryNumber && !!errors.inventoryNumber }
                                helperText={touched.inventoryNumber && errors.inventoryNumber }
                                sx={{ gridColumn: "span 1" }}
                            />
                        </Box>
                        <Box display="flex" justifyContent="end" mt="20px">
                            <Button type="submit" color="secondary" variant="contained">
                                Create New Product
                            </Button>
                        </Box>
                    </form>
                )}
            </Formik>
        </Box>
    )
}

export default ProductCreateForm

