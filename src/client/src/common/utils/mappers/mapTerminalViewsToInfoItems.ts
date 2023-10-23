import { InfoItem } from "common/types/infoItem";

import { TerminalView } from "common/types/terminals/terminalView";

export const mapTerminalViewToInfoItem = (terminal: TerminalView): InfoItem => {
  const infoItem = {
    id: terminal.id,
    name: terminal.name,
    descriptors: {
      // Predicate: (
      //   <Text
      //     as={"a"}
      //     href={terminal.predicate?.iri}
      //     target={"_blank"}
      //     rel={"noopener noreferrer"}
      //     variant={"body-small"}
      //     color={"inherit"}
      //   >
      //     // {terminal.predicate?.iri}
      //   </Text>
      // ),
    },
  };

  // const attributeHasUnits = terminal.units.length > 0;
  // if (attributeHasUnits) {
  //   return {
  //     ...infoItem,
  //     descriptors: {
  //       ...infoItem.descriptors,
  //       Units: terminal.units
  //         .map((x) => x.name)
  //         .sort((a, b) => a.localeCompare(b))
  //         .join(", "),
  //     },
  //   };
  // }

  return infoItem;
};

export const mapTerminalViewsToInfoItems = (attributes: TerminalView[]): InfoItem[] =>
  attributes.map(mapTerminalViewToInfoItem);
