import { YupShape } from "types/yupShape";
import * as yup from "yup";
import { FormApproval } from "./formApproval";

export const approvalSchema = () =>
  yup.object<YupShape<FormApproval>>({
    id: yup.string().required("An approval must have an id"),
    objectType: yup.string().required("An approval must be of a type"),
    state: yup.object().required("An approval must have a state"),
    companyId: yup.number().required("An approval must have a company id"),
  });
