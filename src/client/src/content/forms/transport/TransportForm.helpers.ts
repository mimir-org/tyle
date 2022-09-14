import { TFunction } from "react-i18next";
import { useParams } from "react-router-dom";
import * as yup from "yup";
import { useCreateTransport, useGetTransport, useUpdateTransport } from "../../../data/queries/tyle/queriesTransport";
import { FormTransportLib } from "./types/formTransportLib";

export const useTransportQuery = () => {
  const { id } = useParams();
  return useGetTransport(id);
};

export const useTransportMutation = (isEdit?: boolean) => {
  const createMutation = useCreateTransport();
  const updateMutation = useUpdateTransport();
  return isEdit ? updateMutation : createMutation;
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormTransportLib) => void) => {
  resetField("attributeIdList");
};

export const transportSchema = (t: TFunction<"translation">) =>
  yup.object({
    name: yup.string().max(60, t("transport.validation.name.max")).required(t("transport.validation.name.required")),
    rdsName: yup.string().required(t("transport.validation.rdsName.required")),
    rdsCode: yup.string().required(t("transport.validation.rdsCode.required")),
    purposeName: yup.string().required(t("transport.validation.purposeName.required")),
    aspect: yup.number().required(t("transport.validation.aspect.required")),
    companyId: yup.number().min(1, t("transport.validation.companyId.min")).required(),
    terminalId: yup.string().required(t("transport.validation.terminalId.required")),
    description: yup.string().max(500, t("transport.validation.description.max")),
    parentId: yup.string().nullable(),
    attributeIdList: yup.array().of(
      yup.object().shape({
        value: yup.string().required(t("transport.validation.attributeIdList.value.required")),
      })
    ),
    typeReferences: yup.array().of(
      yup.object().shape({
        name: yup.string().required(t("transport.validation.typeReferences.name.required")),
      })
    ),
  });
