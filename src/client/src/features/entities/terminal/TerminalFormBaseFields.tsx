import { Button } from "complib/buttons";
import { FormField } from "complib/form";
import { Input, Textarea } from "complib/inputs";
import { Flexbox } from "complib/layouts";
import { PlainLink } from "features/common/plain-link";
import { TerminalFormPreview } from "features/entities/entityPreviews/terminal/TerminalFormPreview";
import { FormTerminalLib } from "features/entities/terminal/types/formTerminalLib";
import { useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Text } from "../../../complib/text";
import { FormBaseFieldsContainer } from "complib/form/FormContainer.styled";
interface TerminalFormBaseFieldsProps {
  limited?: boolean;
}

/**
 * Component which contains all simple value fields of the terminal form.
 *
 * @param mode
 * @constructor
 */
export const TerminalFormBaseFields = ({ limited }: TerminalFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, formState } = useFormContext<FormTerminalLib>();
  const { errors } = formState;

  return (
    <FormBaseFieldsContainer>
      <Text variant={"display-small"}>{t("terminal.title")}</Text>
      <TerminalFormPreview control={control} />
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("terminal.name")} error={errors.name}>
          <Input placeholder={t("terminal.placeholders.name")} {...register("name")} disabled={limited} />
        </FormField>

        <FormField label={t("terminal.color")} error={errors.color}>
          <Input type={"color"} placeholder={t("terminal.placeholders.color")} {...register("color")} />
        </FormField>

        <FormField label={t("terminal.description")} error={errors.description}>
          <Textarea placeholder={t("terminal.placeholders.description")} {...register("description")} />
        </FormField>
      </Flexbox>

      <Flexbox justifyContent={"center"} gap={theme.tyle.spacing.xl}>
        <PlainLink tabIndex={-1} to={"/"}>
          <Button tabIndex={0} as={"span"} variant={"outlined"}>
            {t("common.cancel")}
          </Button>
        </PlainLink>
        <Button type={"submit"}>{t("common.submit")}</Button>
      </Flexbox>
    </FormBaseFieldsContainer>
  );
};
