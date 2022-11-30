import { YupShape } from "common/types/yupShape";
import { typeReferenceListSchema } from "features/entities/common/validation/typeReferenceListSchema";
import { TFunction } from "react-i18next";
import * as yup from "yup";
import { FormNodeLib } from "./types/formNodeLib";

export const nodeSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormNodeLib>>({
    name: yup.string().max(60, t("node.validation.name.max")).required(t("node.validation.name.required")),
    rdsName: yup.string().required(t("node.validation.rdsName.required")),
    rdsCode: yup.string().required(t("node.validation.rdsCode.required")),
    purposeName: yup.string().required(t("node.validation.purposeName.required")),
    aspect: yup.number().required(t("node.validation.aspect.required")),
    companyId: yup.number().min(1, t("node.validation.companyId.min")).required(),
    description: yup.string().max(500, t("node.validation.description.max")),
    symbol: yup.string(),
    parentId: yup.string().nullable(),
    nodeTerminals: yup
      .array()
      .of(
        yup.object().shape({
          terminalId: yup.string().required(t("node.validation.nodeTerminals.terminalId.required")),
          connectorDirection: yup.number().required(t("node.validation.nodeTerminals.direction.required")),
          maxQuantity: yup.number().min(0, t("node.validation.nodeTerminals.maxQuantity.min")),
          minQuantity: yup.number().min(0, t("node.validation.nodeTerminals.minQuantity.min")),
        })
      )
      .test("Uniqueness", t("node.validation.nodeTerminals.array.unique"), (terminals) => {
        const uniqueTerminalAndDirectionCombinations = new Set(
          terminals?.map((x) => `${x.terminalId}${x.connectorDirection}`)
        );
        return terminals?.length === uniqueTerminalAndDirectionCombinations.size;
      }),
    attributes: yup.array().nullable(),
    typeReferences: typeReferenceListSchema(t("common.validation.typeReferences.name.required")),
  });
