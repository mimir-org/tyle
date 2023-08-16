import { Button, Flexbox, FormBaseFieldsContainer, FormField, Input, Textarea, Text } from "@mimirorg/component-library";
import { PlainLink } from "features/common/plain-link";
import { TerminalFormPreview } from "features/entities/entityPreviews/terminal/TerminalFormPreview";
import { FormTerminalLib } from "features/entities/terminal/types/formTerminalLib";
import { useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormMode } from "../types/formMode";
interface TerminalFormBaseFieldsProps {
  mode?: FormMode;
  limited?: boolean;
}

/**
 * Component which contains all simple value fields of the terminal form.
 *
 * @param mode
 * @param limited
 * @constructor
 */
export const TerminalFormBaseFields = ({ mode, limited }: TerminalFormBaseFieldsProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, formState } = useFormContext<FormTerminalLib>();
  const { errors } = formState;

  return (
    <FormBaseFieldsContainer>
      <Text variant={"display-small"}>{t("terminal.title")}</Text>
      <TerminalFormPreview control={control} />
      <Flexbox flexDirection={"column"} gap={theme.mimirorg.spacing.l}>
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
