import {
  Button,
  Flexbox,
  FormBaseFieldsContainer,
  FormField,
  Input,
  Text,
  Textarea,
} from "@mimirorg/component-library";
import { FormMode } from "common/types/formMode";
import { useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import PlainLink from "../PlainLink";
import { FormAttributeGroupLib } from "./formAttributeGroupLib";
interface AttributeGroupFormBaseFieldsProps {
  mode?: FormMode;
  limited?: boolean;
}

/**
 * Component which contains all simple value fields of the AttributeGroup form.
 *
 * @param mode
 * @param limited
 * @constructor
 */
const AttributeGroupFormBaseFields = ({ mode, limited }: AttributeGroupFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { register, formState } = useFormContext<FormAttributeGroupLib>();
  const { errors } = formState;

  return (
    <FormBaseFieldsContainer>
      <Text variant={"display-small"}>{t("attributeGroup.title")}</Text>
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l}>
        <FormField label={t("attributeGroup.name")} error={errors.name}>
          <Input placeholder={t("attributeGroup.placeholders.name")} {...register("name")} disabled={limited} />
        </FormField>

        <FormField label={t("attributeGroup.description")} error={errors.description}>
          <Textarea placeholder={t("attributeGroup.placeholders.description")} {...register("description")} />
        </FormField>
      </Flexbox>

      <Flexbox justifyContent={"center"} gap={theme.mimirorg.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            {t("common.cancel")}
          </Button>
        </PlainLink>
        <Button type={"submit"}>{mode === "edit" ? t("common.edit") : t("common.submit")}</Button>
      </Flexbox>
    </FormBaseFieldsContainer>
  );
};

export default AttributeGroupFormBaseFields;
