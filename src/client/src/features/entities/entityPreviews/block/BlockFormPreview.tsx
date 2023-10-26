// import { getColorFromAspect } from "common/utils/getColorFromAspect";
// //import { useGetTerminals } from "external/sources/terminal/terminal.queries";
// //import { getTerminalItemsFromFormData } from "features/entities/entityPreviews/block/BlockFormPreview.helpers";

// import { Control, useWatch } from "react-hook-form";
// import { useTranslation } from "react-i18next";
// import { BlockPreview } from "./BlockPreview";
// import { BlockFormFields } from "features/entities/block/BlockForm.helpers";

// interface BlockFormPreviewProps {
//   control: Control<BlockFormFields>;
// }

// export const BlockFormPreview = ({ control }: BlockFormPreviewProps) => {
//   const { t } = useTranslation("entities");
//   //const terminalQuery = useGetTerminals();

//   const name = useWatch({ control, name: "name" });
//   const symbol = useWatch({ control, name: "symbol" });
//   const aspect = useWatch({ control, name: "aspect" });
//   //const blockTerminals = useWatch({ control, name: "blockTerminals" });

//   return (
//     <BlockPreview
//       variant={"large"}
//       name={name ? name : t("block.name")}
//       img={symbol}
//       color={getColorFromAspect(aspect)}
//       terminals={[] /*getTerminalItemsFromFormData(blockTerminals, terminalQuery.data)*/}
//     />
//   );
// };
