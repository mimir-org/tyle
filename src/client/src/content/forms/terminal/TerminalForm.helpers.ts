import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { TFunction } from "react-i18next";
import { useParams } from "react-router-dom";
import * as yup from "yup";
import { useCreateTerminal, useGetTerminal, useUpdateTerminal } from "../../../data/queries/tyle/queriesTerminal";

export const useTerminalQuery = () => {
  const { id } = useParams();
  return useGetTerminal(id);
};

export const useTerminalMutation = (isEdit?: boolean) => {
  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal();
  return isEdit ? updateMutation : createMutation;
};

export const prepareAttributes = (attributes?: AttributeLibCm[]) => {
  if (!attributes || attributes.length == 0) return [];

  return attributes.sort((a, b) => a.discipline - b.discipline);
};

export const terminalSchema = (t: TFunction<"translation">) =>
  yup.object({
    name: yup.string().max(60, t("terminal.validation.name.max")).required(t("terminal.validation.name.required")),
    color: yup.string().required(t("terminal.validation.color.required")),
    companyId: yup.number().min(1, t("terminal.validation.companyId.min")).required(),
    description: yup.string().max(500, t("terminal.validation.description.max")),
    parentId: yup.string().nullable(),
    attributeIdList: yup.array().of(
      yup.object().shape({
        value: yup.string().required(t("terminal.validation.attributeIdList.value.required")),
      })
    ),
    typeReferences: yup.array().of(
      yup.object().shape({
        name: yup.string().required(t("terminal.validation.typeReferences.name.required")),
      })
    ),
  });
