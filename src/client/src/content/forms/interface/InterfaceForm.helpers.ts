import { TFunction } from "react-i18next";
import { useParams } from "react-router-dom";
import * as yup from "yup";
import { useCreateInterface, useGetInterface, useUpdateInterface } from "../../../data/queries/tyle/queriesInterface";
import { FormInterfaceLib } from "./types/formInterfaceLib";

export const useInterfaceQuery = () => {
  const { id } = useParams();
  return useGetInterface(id);
};

export const useInterfaceMutation = (isEdit?: boolean) => {
  const createMutation = useCreateInterface();
  const updateMutation = useUpdateInterface();
  return isEdit ? updateMutation : createMutation;
};

/**
 * Resets the part of form which is dependent on initial choices, e.g. aspect
 *
 * @param resetField
 */
export const resetSubform = (resetField: (value: keyof FormInterfaceLib) => void) => {
  resetField("attributeIdList");
};

export const interfaceSchema = (t: TFunction<"translation">) =>
  yup.object({
    name: yup.string().max(60, t("interface.validation.name.max")).required(t("interface.validation.name.required")),
    rdsName: yup.string().required(t("interface.validation.rdsName.required")),
    rdsCode: yup.string().required(t("interface.validation.rdsCode.required")),
    purposeName: yup.string().required(t("interface.validation.purposeName.required")),
    aspect: yup.number().required(t("interface.validation.aspect.required")),
    companyId: yup.number().min(1, t("interface.validation.companyId.min")).required(),
    terminalId: yup.string().required(t("interface.validation.terminalId.required")),
    description: yup.string().max(500, t("interface.validation.description.max")),
    parentId: yup.string().nullable(),
    attributeIdList: yup.array().of(
      yup.object().shape({
        value: yup.string().required(t("interface.validation.attributeIdList.value.required")),
      })
    ),
    typeReferences: yup.array().of(
      yup.object().shape({
        name: yup.string().required(t("interface.validation.typeReferences.name.required")),
      })
    ),
  });
