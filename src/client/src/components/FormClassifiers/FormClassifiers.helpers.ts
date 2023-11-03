import { RdlClassifier } from "types/common/rdlClassifier";
import { InfoItem } from "types/infoItem";

export const classifierInfoItem = (classifier: RdlClassifier): InfoItem => ({
  id: classifier.id.toString(),
  name: classifier.name,
  descriptors: {
    Description: classifier.description,
    IRI: classifier.iri,
  },
});
