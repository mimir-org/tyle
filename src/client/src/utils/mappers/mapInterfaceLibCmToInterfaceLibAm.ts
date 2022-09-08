import { InterfaceLibAm, InterfaceLibCm } from "@mimirorg/typelibrary-types";

export const mapInterfaceLibCmToInterfaceLibAm = (item: InterfaceLibCm): InterfaceLibAm => ({
  ...item,
  attributeIdList: item.attributes.map((x) => x.id),
  parentId: item.parentId,
});
