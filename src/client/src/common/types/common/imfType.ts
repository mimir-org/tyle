export interface ImfType {
  id: string;
  name: string;
  description: string | undefined;
  version: string;
  createdOn: Date;
  createdBy: string;
  contributedBy: string[];
  lastUpdateOn: Date;
}