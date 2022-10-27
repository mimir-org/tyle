import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Box } from "complib/layouts";
import { Text } from "complib/text";
import { SearchField } from "../../../common/components/search-field";
import { ItemListProps } from "../search/components/item/ItemList";
import { Default as ItemList } from "../search/components/item/ItemList.stories";
import { ExploreSection } from "./ExploreSection";

export default {
  title: "Explore/Common/ExploreSection",
  component: ExploreSection,
} as ComponentMeta<typeof ExploreSection>;

const Template: ComponentStory<typeof ExploreSection> = (args) => <ExploreSection {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Section",
  children: "Simple text content in section",
};

export const WithCustomContent = Template.bind({});
WithCustomContent.args = {
  title: "Section",
  children: (
    <Box display={"flex"} flexDirection={"column"} gap={"16px"} width={"500px"}>
      <Text variant={"title-large"}>Custom content</Text>
      <SearchField />
      <Text variant={"label-large"}>We found several items!</Text>
      <ItemList {...(ItemList.args as ItemListProps)} />
    </Box>
  ),
};
