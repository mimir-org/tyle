import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";
import { FormAspectObjectLib } from "./types/formAspectObjectLib";

export const aspectObjectSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormAspectObjectLib>>({
    name: yup
      .string()
      .max(60, t("aspectObject.validation.name.max"))
      .required(t("aspectObject.validation.name.required")),
    rdsId: yup.string().required(t("aspectObject.validation.rdsId.required")),
    purposeName: yup.string().required(t("aspectObject.validation.purposeName.required")),
    aspect: yup.number().required(t("aspectObject.validation.aspect.required")),
    companyId: yup.number().min(1, t("aspectObject.validation.companyId.min")).required(),
    description: yup.string().max(500, t("aspectObject.validation.description.max")),
    symbol: yup.string(),
    parentId: yup.string().nullable(),
    aspectObjectTerminals: yup
      .array()
      .of(
        yup.object().shape({
          terminalId: yup.string().required(t("aspectObject.validation.aspectObjectTerminals.terminalId.required")),
          connectorDirection: yup
            .number()
            .required(t("aspectObject.validation.aspectObjectTerminals.direction.required")),
          maxQuantity: yup.number().min(0, t("aspectObject.validation.aspectObjectTerminals.maxQuantity.min")),
          minQuantity: yup.number().min(0, t("aspectObject.validation.aspectObjectTerminals.minQuantity.min")),
        })
      )
      .test("Uniqueness", t("aspectObject.validation.aspectObjectTerminals.array.unique"), (terminals) => {
        const uniqueTerminalAndDirectionCombinations = new Set(
          terminals?.map((x) => `${x.terminalId}${x.connectorDirection}`)
        );
        return terminals?.length === uniqueTerminalAndDirectionCombinations.size;
      }),
    attributes: yup.array().nullable(),
    typeReference: yup.string(),
  });
