import { YupShape } from "common/types/yupShape";
import { TFunction } from "react-i18next";
import * as yup from "yup";
import { FormAspectObjectLib } from "./types/formAspectObjectLib";

export const aspectObjectSchema = (t: TFunction<"translation">) =>
  yup.object<YupShape<FormAspectObjectLib>>({
    name: yup.string().max(120, t("common.validation.name.max")).required(t("common.validation.name.required")),
    typeReference: yup.string().max(255),
    version: yup.string().max(7),
    companyId: yup.number().min(1, t("aspectObject.validation.companyId.min")).required(t("aspectObject.validation.companyId.required")),
    aspect: yup.number().required(t("aspectObject.validation.aspect.required")),
    purposeName: yup.string().max(127).required(t("aspectObject.validation.purposeName.required")),
    rdsId: yup.string().required(t("aspectObject.validation.rdsId.required")),
    symbol: yup.string().max(127),
    description: yup.string().max(500, t("common.validation.description.max")),
    aspectObjectTerminals: yup
      .array()
      .of(
        yup.object().shape({
          terminalId: yup.string().required(t("aspectObject.validation.aspectObjectTerminals.terminalId.required")),
          connectorDirection: yup
            .number()
            .required(t("aspectObject.validation.aspectObjectTerminals.direction.required")),
          maxQuantity: yup.number().min(0, t("aspectObject.validation.aspectObjectTerminals.maxQuantity.min")).required(),
          minQuantity: yup.number().min(0, t("aspectObject.validation.aspectObjectTerminals.minQuantity.min")).required(),
        })
      )
      .test("Uniqueness", t("aspectObject.validation.aspectObjectTerminals.array.unique"), (terminals) => {
        const uniqueTerminalAndDirectionCombinations = new Set(
          terminals?.map((x) => `${x.terminalId}${x.connectorDirection}`)
        );
        return terminals?.length === uniqueTerminalAndDirectionCombinations.size;
      }),
    attributes: yup.array().nullable(),
  });
