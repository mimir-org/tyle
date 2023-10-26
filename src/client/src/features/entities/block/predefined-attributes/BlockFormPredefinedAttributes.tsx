// import { Aspect } from "@mimirorg/typelibrary-types";
// import { Box, FormField, Gridbox, Input, Select } from "@mimirorg/component-library";
// import { useGetAttributesPredefined } from "external/sources/attribute/attribute.queries";
// import { FormSection } from "features/entities/common/form-section/FormSection";
// import { preparePredefinedAttributes } from "features/entities/block/predefined-attributes/BlockFormPredefinedAttributes.helpers";
// // import { FormBlockLib } from "features/entities/block/types/formBlockLib";
// import { Controller, useFormContext } from "react-hook-form";
// import { useTranslation } from "react-i18next";
// import { useTheme } from "styled-components/macro";

// export interface BlockFormPredefinedAttributesProps {
//   aspects?: Aspect[];
// }

// export const BlockFormPredefinedAttributes = ({ aspects }: BlockFormPredefinedAttributesProps) => {
//   const theme = useTheme();
//   const { t } = useTranslation("entities");
//   const { control, register } = useFormContext<FormBlockLib>();

//   const predefinedAttributesQuery = useGetAttributesPredefined();
//   const predefinedAttributes = preparePredefinedAttributes(predefinedAttributesQuery.data, aspects);

//   return (
//     <FormSection title={t("block.predefinedAttributes.title")}>
//       <Gridbox gridTemplateColumns={"repeat(auto-fill, 300px)"} gap={theme.mimirorg.spacing.xl}>
//         {predefinedAttributes.map((x, index) => {
//           return (
//             <Box key={x.key}>
//               <Input {...register(`selectedAttributePredefined.${index}.key`)} type={"hidden"} value={x.key} />
//               <FormField label={x.key}>
//                 <Controller
//                   control={control}
//                   name={`selectedAttributePredefined.${index}.values`}
//                   render={({ field: { ref, onChange, ...rest } }) => (
//                     <Select
//                       {...rest}
//                       selectRef={ref}
//                       placeholder={t("block.predefinedAttributes.placeholders.attribute")}
//                       options={x.valueStringList.map((y) => ({ value: y }))}
//                       getOptionLabel={(y) => y.value}
//                       isLoading={predefinedAttributesQuery.isLoading}
//                       isMulti={x.isMultiSelect}
//                       onChange={(val) => {
//                         if (!Array.isArray(val)) onChange([val]);
//                         else onChange(val);
//                       }}
//                     />
//                   )}
//                 />
//               </FormField>
//             </Box>
//           );
//         })}
//       </Gridbox>
//     </FormSection>
//   );
// };
