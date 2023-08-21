import { Meta, StoryFn } from "@storybook/react";
import { Box, Text } from "@mimirorg/component-library";
import { SearchField } from "features/common/search-field";
import { ExploreSection } from "features/explore/common/ExploreSection";
import { ItemListProps } from "features/explore/search/components/item/ItemList";
import { Default as ItemList } from "features/explore/search/components/item/ItemList.stories";

export default {
  title: "Features/Explore/Common/ExploreSection",
  component: ExploreSection,
} as Meta<typeof ExploreSection>;

const Template: StoryFn<typeof ExploreSection> = (args) => <ExploreSection {...args} />;

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
