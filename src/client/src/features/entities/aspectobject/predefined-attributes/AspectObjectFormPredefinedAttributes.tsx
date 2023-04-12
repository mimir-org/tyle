import { Aspect } from "@mimirorg/typelibrary-types";
import { FormField } from "complib/form";
import { Input, Select } from "complib/inputs";
import { Box, Grid } from "complib/layouts";
import { useGetAttributesPredefined } from "external/sources/attribute/attribute.queries";
import { FormSection } from "features/entities/common/form-section/FormSection";
import { preparePredefinedAttributes } from "features/entities/aspectobject/predefined-attributes/AspectObjectFormPredefinedAttributes.helpers";
import { FormApectObjectLib } from "features/entities/aspectobject/types/formAspectObjectLib";
import { Controller, useFormContext } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";

export interface AspectObjectFormPredefinedAttributesProps {
  aspects?: Aspect[];
}

export const AspectObjectFormPredefinedAttributes = ({ aspects }: AspectObjectFormPredefinedAttributesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("entities");
  const { control, register } = useFormContext<FormAspectObjectLib>();

  const predefinedAttributesQuery = useGetAttributesPredefined();
  const predefinedAttributes = preparePredefinedAttributes(predefinedAttributesQuery.data, aspects);

  return (
    <FormSection title={t("aspectObject.predefinedAttributes.title")}>
      <Grid gridTemplateColumns={"repeat(auto-fill, 300px)"} gap={theme.tyle.spacing.xl}>
        {predefinedAttributes.map((x, index) => {
          return (
            <Box key={x.key}>
              <Input {...register(`selectedAttributePredefined.${index}.key`)} type={"hidden"} value={x.key} />
              <FormField label={x.key}>
                <Controller
                  control={control}
                  name={`selectedAttributePredefined.${index}.values`}
                  render={({ field: { ref, onChange, ...rest } }) => (
                    <Select
                      {...rest}
                      selectRef={ref}
                      placeholder={t("aspectObject.predefinedAttributes.placeholders.attribute")}
                      options={x.valueStringList.map((y) => ({ value: y }))}
                      getOptionLabel={(y) => y.value}
                      isLoading={predefinedAttributesQuery.isLoading}
                      isMulti={x.isMultiSelect}
                      onChange={(val) => {
                        if (!Array.isArray(val)) onChange([val]);
                        else onChange(val);
                      }}
                    />
                  )}
                />
              </FormField>
            </Box>
          );
        })}
      </Grid>
    </FormSection>
  );
};
