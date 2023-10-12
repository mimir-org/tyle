export interface ImfType {
  id: string;
  name: string;
  description?: string;
  version: string;
  createdOn: Date;
  createdBy: string;
  contributedBy: string[];
  lastUpdateOn: Date;
}
