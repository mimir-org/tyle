import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { useGetFilteredCompanies } from "common/hooks/filter-companies/useGetFilteredCompanies";
import { Button } from "complib/buttons";
import { FormField } from "complib/form";
import { Input, Select, Textarea } from "complib/inputs";
import { Flexbox } from "complib/layouts";
import { PlainLink } from "features/common/plain-link";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components";
import { FormAttributeLib } from "./types/formAttributeLib";
import { AttributeFormContainer } from "./AttributeFormContainer.styled";

/**
 * Component which contains all simple value fields of the attribute form.
 *
 * @constructor
 */
export const AttributeFormBaseFields = () => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register, formState } = useFormContext<FormAttributeLib>();
  const { errors } = formState;

  const companies = useGetFilteredCompanies(MimirorgPermission.Write);

  return (
    <AttributeFormContainer>
      {/** <TerminalFormPreview control={control} /> */}

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.l}>
        <FormField label={t("attribute.name")} error={errors.name}>
          <Input placeholder={t("attribute.placeholders.name")} {...register("name")} disabled={false} />
        </FormField>

        <Controller
          control={control}
          name={"companyId"}
          render={({ field: { value, onChange, ref, ...rest } }) => (
            <FormField label={t("attribute.owner")} error={errors.companyId}>
              <Select
                {...rest}
                selectRef={ref}
                placeholder={t("common.templates.select", { object: t("attribute.owner").toLowerCase() })}
                options={companies}
                getOptionLabel={(x) => x.name}
                getOptionValue={(x) => x.id.toString()}
                onChange={(x) => {
                  onChange(x?.id);
                }}
                value={companies.find((x) => x.id === value)}
                isDisabled={false}
              />
            </FormField>
          )}
        />

        <FormField label={t("attribute.description")} error={errors.description}>
          <Textarea placeholder={t("attribute.placeholders.description")} {...register("description")} />
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
    </AttributeFormContainer>
  );
};
