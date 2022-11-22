import { ComponentStory } from "@storybook/react";
import { SearchField } from "common/components/search-field/SearchField";

export default {
  title: "Common/SearchField",
  component: SearchField,
};

const Template: ComponentStory<typeof SearchField> = (args) => <SearchField {...args} />;

export const Default = Template.bind({});
