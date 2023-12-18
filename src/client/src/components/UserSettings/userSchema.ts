import { UserRequest } from "types/authentication/userRequest";
import { YupShape } from "types/yupShape";
import * as yup from "yup";

export const userSchema = () =>
  yup.object<YupShape<UserRequest>>({
    firstName: yup.string().required("Please enter a first name"),
    lastName: yup.string().required("Please enter a last name"),
  });
