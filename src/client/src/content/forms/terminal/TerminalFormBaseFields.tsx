import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { Button } from "../../../complib/buttons";
import { FormField } from "../../../complib/form";
import { Input, Select, Textarea } from "../../../complib/inputs";
import { Flexbox } from "../../../complib/layouts";
import { PlainLink } from "../../utils/PlainLink";
import { useGetFilteredCompanies } from "../common/utils/useGetFilteredCompanies";
import { TerminalFormBaseFieldsContainer } from "./TerminalFormBaseFields.styled";
import { TerminalFormPreview } from "./TerminalFormPreview";
import { FormTerminalLib } from "./types/formTerminalLib";

/**
 * Component which contains all simple value fields of the terminal form.
 */
export const TerminalFormBaseFields = () => {
  const theme = useTheme();
  const { t } = useTranslation();
  const { control, register, formState } = useFormContext<FormTerminalLib>();
  const { errors } = formState;

  const companies = useGetFilteredCompanies(MimirorgPermission.Write);

  return (
    <TerminalFormBaseFieldsContainer>
      <TerminalFormPreview control={control} />

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("terminal.name")} error={errors.name}>
          <Input placeholder={t("terminal.placeholders.name")} {...register("name")} />
        </FormField>

        <FormField label={t("terminal.color")} error={errors.color}>
          <Input type={"color"} placeholder={t("terminal.placeholders.color")} {...register("color")} />
        </FormField>

        <Controller
          control={control}
          name={"companyId"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("terminal.owner")} error={errors.companyId}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("terminal.owner").toLowerCase() })}
                options={companies}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.id.toString()}
                onChange={(x) => {
                  onChange(x?.id);
                }}
                value={companies.find((x) => x.id === value)}
              />
            </FormField>
          )}
        />

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
    </TerminalFormBaseFieldsContainer>
  );
};
