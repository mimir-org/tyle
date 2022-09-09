import { Aspect } from "@mimirorg/typelibrary-types";
import { Control, Controller, UseFormRegister } from "react-hook-form";
import { useTranslation } from "react-i18next";
import { useTheme } from "styled-components/macro";
import { FormField } from "../../../../complib/form";
import { Input, Select } from "../../../../complib/inputs";
import { Box, Grid } from "../../../../complib/layouts";
import { useGetAttributesPredefined } from "../../../../data/queries/tyle/queriesAttribute";
import { FormSection } from "../../common/form-section/FormSection";
import { FormNodeLib } from "../types/formNodeLib";
import { preparePredefinedAttributes } from "./NodeFormPredefinedAttributes.helpers";

export interface NodeFormPredefinedAttributesProps {
  control: Control<FormNodeLib>;
  register: UseFormRegister<FormNodeLib>;
  aspects?: Aspect[];
}

export const NodeFormPredefinedAttributes = ({ control, register, aspects }: NodeFormPredefinedAttributesProps) => {
  const theme = useTheme();
  const { t } = useTranslation("translation", { keyPrefix: "predefinedAttributes" });

  const predefinedAttributesQuery = useGetAttributesPredefined();
  const predefinedAttributes = preparePredefinedAttributes(predefinedAttributesQuery.data, aspects);

  return (
    <FormSection title={t("title")}>
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
                      placeholder={t("placeholders.attribute")}
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
