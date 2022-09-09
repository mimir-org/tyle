import { TransportLibAm, TransportLibCm } from "@mimirorg/typelibrary-types";

export const mapTransportLibCmToTransportLibAm = (transport: TransportLibCm): TransportLibAm => ({
  ...transport,
  attributeIdList: transport.attributes.map((x) => x.id),
  parentId: transport.parentId,
});
